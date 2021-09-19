// Originally from https://stackoverflow.com/a/49889680/7527745
// Adapted from https://stackoverflow.com/a/32243171/645283
namespace InstagramApiSharp.Helpers
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;
#pragma warning disable IDE0059 // Unnecessary assignment of a value
#pragma warning disable IDE0051 // Remove unused private members

    public class PemKeyUtils
    {
        const string pemprivheader = "-----BEGIN RSA PRIVATE KEY-----";
        const string pemprivfooter = "-----END RSA PRIVATE KEY-----";
        const string pempubheader = "-----BEGIN PUBLIC KEY-----";
        const string pempubfooter = "-----END PUBLIC KEY-----";
        const string pemp8header = "-----BEGIN PRIVATE KEY-----";
        const string pemp8footer = "-----END PRIVATE KEY-----";
        const string pemp8encheader = "-----BEGIN ENCRYPTED PRIVATE KEY-----";
        const string pemp8encfooter = "-----END ENCRYPTED PRIVATE KEY-----";
        public static bool Verbose = false;

        public static RSACryptoServiceProvider GetRSAProviderFromPemString(string pemstr)
        {
            bool isPrivateKeyFile = true;

            if (pemstr.StartsWith(pempubheader) && pemstr.EndsWith(pempubfooter))
                isPrivateKeyFile = false;

            byte[] pemkey;
            if (isPrivateKeyFile)
                pemkey = DecodeOpenSSLPrivateKey(pemstr);
            else
                pemkey = DecodeOpenSSLPublicKey(pemstr);

            if (pemkey == null)
                return null;

            if (isPrivateKeyFile)
                return DecodeRSAPrivateKey(pemkey);
            else
                return DecodeX509PublicKey(pemkey);

        }

        //--------   Get the binary RSA PUBLIC key   --------
        static byte[] DecodeOpenSSLPublicKey(string instr)
        {
            const string pempubheader = "-----BEGIN PUBLIC KEY-----";
            const string pempubfooter = "-----END PUBLIC KEY-----";
            string pemstr = instr.Trim();
            byte[] binkey;
            if (!pemstr.StartsWith(pempubheader) || !pemstr.EndsWith(pempubfooter))
                return null;
            StringBuilder sb = new StringBuilder(pemstr);
            sb.Replace(pempubheader, "");  //remove headers/footers, if present
            sb.Replace(pempubfooter, "");

            string pubstr = sb.ToString().Trim();   //get string after removing leading/trailing whitespace

            try
            {
                binkey = Convert.FromBase64String(pubstr);
            }
            catch (FormatException)
            {       //if can't b64 decode, data is not valid
                return null;
            }
            return binkey;
        }

        static RSACryptoServiceProvider DecodeX509PublicKey(byte[] x509Key)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            using (var mem = new MemoryStream(x509Key))
            {
                using (var binr = new BinaryReader(mem))    //wrap Memory Stream with BinaryReader for easy reading
                {
                    try
                    {
                        var twobytes = binr.ReadUInt16();
                        switch (twobytes)
                        {
                            case 0x8130:
                                binr.ReadByte();    //advance 1 byte
                                break;
                            case 0x8230:
                                binr.ReadInt16();   //advance 2 bytes
                                break;
                            default:
                                return null;
                        }

                        var seq = binr.ReadBytes(15);
                        if (!CompareBytearrays(seq, seqOid))  //make sure Sequence for OID is correct
                            return null;

                        twobytes = binr.ReadUInt16();
                        if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                            binr.ReadByte();    //advance 1 byte
                        else if (twobytes == 0x8203)
                            binr.ReadInt16();   //advance 2 bytes
                        else
                            return null;

                        var bt = binr.ReadByte();
                        if (bt != 0x00)     //expect null byte next
                            return null;

                        twobytes = binr.ReadUInt16();
                        if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                            binr.ReadByte();    //advance 1 byte
                        else if (twobytes == 0x8230)
                            binr.ReadInt16();   //advance 2 bytes
                        else
                            return null;

                        twobytes = binr.ReadUInt16();
                        byte lowbyte = 0x00;
                        byte highbyte = 0x00;

                        if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                            lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                        else if (twobytes == 0x8202)
                        {
                            highbyte = binr.ReadByte(); //advance 2 bytes
                            lowbyte = binr.ReadByte();
                        }
                        else
                            return null;
                        byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                        int modsize = BitConverter.ToInt32(modint, 0);

                        byte firstbyte = binr.ReadByte();
                        binr.BaseStream.Seek(-1, SeekOrigin.Current);

                        if (firstbyte == 0x00)
                        {   //if first byte (highest order) of modulus is zero, don't include it
                            binr.ReadByte();    //skip this null byte
                            modsize -= 1;   //reduce modulus buffer size by 1
                        }

                        byte[] modulus = binr.ReadBytes(modsize); //read the modulus bytes

                        if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                            return null;
                        int expbytes = binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                        byte[] exponent = binr.ReadBytes(expbytes);

                        // We don't really need to print anything but if we insist to...
                        //showBytes("\nExponent", exponent);
                        //showBytes("\nModulus", modulus);

                        // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                        RSAParameters rsaKeyInfo = new RSAParameters
                        {
                            Modulus = modulus,
                            Exponent = exponent
                        };
                        rsa.ImportParameters(rsaKeyInfo);
                        return rsa;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
        }

        //------- Parses binary ans.1 RSA private key; returns RSACryptoServiceProvider  ---
        static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
            MemoryStream mem = new MemoryStream(privkey);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;
            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102) //version number
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;


                //------  all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);

                Console.WriteLine("showing components ..");
                if (Verbose)
                {
                    ShowBytes("\nModulus", MODULUS);
                    ShowBytes("\nExponent", E);
                    ShowBytes("\nD", D);
                    ShowBytes("\nP", P);
                    ShowBytes("\nQ", Q);
                    ShowBytes("\nDP", DP);
                    ShowBytes("\nDQ", DQ);
                    ShowBytes("\nIQ", IQ);
                }

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAparams = new RSAParameters
                {
                    Modulus = MODULUS,
                    Exponent = E,
                    D = D,
                    P = P,
                    Q = Q,
                    DP = DP,
                    DQ = DQ,
                    InverseQ = IQ
                };
                RSA.ImportParameters(RSAparams);
                return RSA;
            }
            catch (Exception)
            {
                return null;
            }
            finally { binr.Close(); }
        }

        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)     //expect integer
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();    // data size in next byte
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte(); // data size in next 2 bytes
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;     // we already have the data size
            }



            while (binr.ReadByte() == 0x00)
            {   //remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);     //last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }

        //-----  Get the binary RSA PRIVATE key, decrypting if necessary ----
        static byte[] DecodeOpenSSLPrivateKey(string instr)
        {
            const string pemprivheader = "-----BEGIN RSA PRIVATE KEY-----";
            const string pemprivfooter = "-----END RSA PRIVATE KEY-----";
            string pemstr = instr.Trim();
            byte[] binkey;
            if (!pemstr.StartsWith(pemprivheader) || !pemstr.EndsWith(pemprivfooter))
                return null;

            StringBuilder sb = new StringBuilder(pemstr);
            sb.Replace(pemprivheader, "");  //remove headers/footers, if present
            sb.Replace(pemprivfooter, "");

            string pvkstr = sb.ToString().Trim();   //get string after removing leading/trailing whitespace

            try
            {        // if there are no PEM encryption info lines, this is an UNencrypted PEM private key
                binkey = Convert.FromBase64String(pvkstr);
                return binkey;
            }
            catch (FormatException)
            {       //if can't b64 decode, it must be an encrypted private key
                    //Console.WriteLine("Not an unencrypted OpenSSL PEM private key");  
            }

            StringReader str = new StringReader(pvkstr);

            //-------- read PEM encryption info. lines and extract salt -----
            if (!str.ReadLine().StartsWith("Proc-Type: 4,ENCRYPTED"))
                return null;
            string saltline = str.ReadLine();
            if (!saltline.StartsWith("DEK-Info: DES-EDE3-CBC,"))
                return null;
            string saltstr = saltline.Substring(saltline.IndexOf(",") + 1).Trim();
            byte[] salt = new byte[saltstr.Length / 2];
            for (int i = 0; i < salt.Length; i++)
                salt[i] = Convert.ToByte(saltstr.Substring(i * 2, 2), 16);
            if (!(str.ReadLine() == ""))
                return null;

            //------ remaining b64 data is encrypted RSA key ----
            string encryptedstr = str.ReadToEnd();

            try
            {   //should have b64 encrypted RSA key now
                binkey = Convert.FromBase64String(encryptedstr);
            }
            catch (System.FormatException)
            {  // bad b64 data.
                return null;
            }

            //------ Get the 3DES 24 byte key using PDK used by OpenSSL ----

            SecureString despswd = GetSecPswd("Enter password to derive 3DES key==>");
            //Console.Write("\nEnter password to derive 3DES key: ");
            //String pswd = Console.ReadLine();
            byte[] deskey = GetOpenSSL3deskey(salt, despswd, 1, 2);    // count=1 (for OpenSSL implementation); 2 iterations to get at least 24 bytes
            if (deskey == null)
                return null;
            //showBytes("3DES key", deskey) ;

            //------ Decrypt the encrypted 3des-encrypted RSA private key ------
            byte[] rsakey = DecryptKey(binkey, deskey, salt); //OpenSSL uses salt value in PEM header also as 3DES IV
            if (rsakey != null)
                return rsakey;  //we have a decrypted RSA private key
            else
            {
                Console.WriteLine("Failed to decrypt RSA private key; probably wrong password.");
                return null;
            }
        }


        // ----- Decrypt the 3DES encrypted RSA private key ----------
        static byte[] DecryptKey(byte[] cipherData, byte[] desKey, byte[] IV)
        {
            MemoryStream memst = new MemoryStream();
            TripleDES alg = TripleDES.Create();
            alg.Key = desKey;
            alg.IV = IV;
            try
            {
                CryptoStream cs = new CryptoStream(memst, alg.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(cipherData, 0, cipherData.Length);
                cs.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                return null;
            }
            byte[] decryptedData = memst.ToArray();
            return decryptedData;
        }

        //-----   OpenSSL PBKD uses only one hash cycle (count); miter is number of iterations required to build sufficient bytes ---
        static byte[] GetOpenSSL3deskey(byte[] salt, SecureString secpswd, int count, int miter)
        {
            IntPtr unmanagedPswd = IntPtr.Zero;
            int HASHLENGTH = 16;    //MD5 bytes
            byte[] keymaterial = new byte[HASHLENGTH * miter];     //to store contatenated Mi hashed results


            byte[] psbytes = new byte[secpswd.Length];
            unmanagedPswd = Marshal.SecureStringToGlobalAllocAnsi(secpswd);
            Marshal.Copy(unmanagedPswd, psbytes, 0, psbytes.Length);
            Marshal.ZeroFreeGlobalAllocAnsi(unmanagedPswd);

            //UTF8Encoding utf8 = new UTF8Encoding();
            //byte[] psbytes = utf8.GetBytes(pswd);

            // --- contatenate salt and pswd bytes into fixed data array ---
            byte[] data00 = new byte[psbytes.Length + salt.Length];
            Array.Copy(psbytes, data00, psbytes.Length);      //copy the pswd bytes
            Array.Copy(salt, 0, data00, psbytes.Length, salt.Length); //concatenate the salt bytes

            // ---- do multi-hashing and contatenate results  D1, D2 ...  into keymaterial bytes ----
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = null;
            byte[] hashtarget = new byte[HASHLENGTH + data00.Length];   //fixed length initial hashtarget

            for (int j = 0; j < miter; j++)
            {
                // ----  Now hash consecutively for count times ------
                if (j == 0)
                    result = data00;    //initialize 
                else
                {
                    Array.Copy(result, hashtarget, result.Length);
                    Array.Copy(data00, 0, hashtarget, result.Length, data00.Length);
                    result = hashtarget;
                    //Console.WriteLine("Updated new initial hash target:") ;
                    //showBytes(result) ;
                }

                for (int i = 0; i < count; i++)
                    result = md5.ComputeHash(result);
                Array.Copy(result, 0, keymaterial, j * HASHLENGTH, result.Length);  //contatenate to keymaterial
            }
            //showBytes("Final key material", keymaterial);
            byte[] deskey = new byte[24];
            Array.Copy(keymaterial, deskey, deskey.Length);

            Array.Clear(psbytes, 0, psbytes.Length);
            Array.Clear(data00, 0, data00.Length);
            Array.Clear(result, 0, result.Length);
            Array.Clear(hashtarget, 0, hashtarget.Length);
            Array.Clear(keymaterial, 0, keymaterial.Length);

            return deskey;
        }

        static SecureString GetSecPswd(string prompt)
        {
            SecureString password = new SecureString();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(prompt);
            Console.ForegroundColor = ConsoleColor.Magenta;

            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    return password;
                }
                else if (cki.Key == ConsoleKey.Backspace)
                {
                    // remove the last asterisk from the screen...
                    if (password.Length > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        password.RemoveAt(password.Length - 1);
                    }
                }
                else if (cki.Key == ConsoleKey.Escape)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine();
                    return password;
                }
                else if (Char.IsLetterOrDigit(cki.KeyChar) || Char.IsSymbol(cki.KeyChar))
                {
                    if (password.Length < 20)
                    {
                        password.AppendChar(cki.KeyChar);
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Beep();
                    }
                }
                else
                {
                    Console.Beep();
                }
            }
        }

        static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        static void ShowBytes(string info, byte[] data)
        {
            Console.WriteLine("{0}  [{1} bytes]", info, data.Length);
            for (int i = 1; i <= data.Length; i++)
            {
                Console.Write("{0:X2}  ", data[i - 1]);
                if (i % 16 == 0)
                    Console.WriteLine();
            }
            Console.WriteLine("\n\n");
        }

        /// <summary>
        /// Export public key from MS RSACryptoServiceProvider into OpenSSH PEM string
        /// slightly modified from https://stackoverflow.com/a/28407693
        /// </summary>
        /// <param name="csp"></param>
        /// <returns></returns>
        public static string ExportPublicKey(RSACryptoServiceProvider csp)
        {
            StringWriter outputStream = new StringWriter();
            var parameters = csp.ExportParameters(false);
            using (var stream = new MemoryStream())
            {
                var writer = new BinaryWriter(stream);
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new MemoryStream())
                {
                    var innerWriter = new BinaryWriter(innerStream);
                    innerWriter.Write((byte)0x30); // SEQUENCE
                    EncodeLength(innerWriter, 13);
                    innerWriter.Write((byte)0x06); // OBJECT IDENTIFIER
                    var rsaEncryptionOid = new byte[] { 0x2a, 0x86, 0x48, 0x86, 0xf7, 0x0d, 0x01, 0x01, 0x01 };
                    EncodeLength(innerWriter, rsaEncryptionOid.Length);
                    innerWriter.Write(rsaEncryptionOid);
                    innerWriter.Write((byte)0x05); // NULL
                    EncodeLength(innerWriter, 0);
                    innerWriter.Write((byte)0x03); // BIT STRING
                    using (var bitStringStream = new MemoryStream())
                    {
                        var bitStringWriter = new BinaryWriter(bitStringStream);
                        bitStringWriter.Write((byte)0x00); // # of unused bits
                        bitStringWriter.Write((byte)0x30); // SEQUENCE
                        using (var paramsStream = new MemoryStream())
                        {
                            var paramsWriter = new BinaryWriter(paramsStream);
                            EncodeIntegerBigEndian(paramsWriter, parameters.Modulus); // Modulus
                            EncodeIntegerBigEndian(paramsWriter, parameters.Exponent); // Exponent
                            var paramsLength = (int)paramsStream.Length;
                            EncodeLength(bitStringWriter, paramsLength);
                            bitStringWriter.Write(paramsStream.GetBuffer(), 0, paramsLength);
                        }
                        var bitStringLength = (int)bitStringStream.Length;
                        EncodeLength(innerWriter, bitStringLength);
                        innerWriter.Write(bitStringStream.GetBuffer(), 0, bitStringLength);
                    }
                    var length = (int)innerStream.Length;
                    EncodeLength(writer, length);
                    writer.Write(innerStream.GetBuffer(), 0, length);
                }

                var base64 = Convert.ToBase64String(stream.GetBuffer(), 0, (int)stream.Length).ToCharArray();
                // WriteLine terminates with \r\n, we want only \n
                outputStream.Write("-----BEGIN PUBLIC KEY-----\n");
                for (var i = 0; i < base64.Length; i += 64)
                {
                    outputStream.Write(base64, i, Math.Min(64, base64.Length - i));
                    outputStream.Write("\n");
                }
                outputStream.Write("-----END PUBLIC KEY-----");
            }

            return outputStream.ToString();
        }

        // https://stackoverflow.com/a/23739932/2860309
        private static void EncodeLength(BinaryWriter stream, int length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "Length must be non-negative");
            if (length < 0x80)
            {
                // Short form
                stream.Write((byte)length);
            }
            else
            {
                // Long form
                var temp = length;
                var bytesRequired = 0;
                while (temp > 0)
                {
                    temp >>= 8;
                    bytesRequired++;
                }
                stream.Write((byte)(bytesRequired | 0x80));
                for (var i = bytesRequired - 1; i >= 0; i--)
                {
                    stream.Write((byte)(length >> (8 * i) & 0xff));
                }
            }
        }

        //https://stackoverflow.com/a/23739932/2860309
        private static void EncodeIntegerBigEndian(BinaryWriter stream, byte[] value, bool forceUnsigned = true)
        {
            stream.Write((byte)0x02); // INTEGER
            var prefixZeros = 0;
            for (var i = 0; i < value.Length; i++)
            {
                if (value[i] != 0) break;
                prefixZeros++;
            }
            if (value.Length - prefixZeros == 0)
            {
                EncodeLength(stream, 1);
                stream.Write((byte)0);
            }
            else
            {
                if (forceUnsigned && value[prefixZeros] > 0x7f)
                {
                    // Add a prefix zero to force unsigned if the MSB is 1
                    EncodeLength(stream, value.Length - prefixZeros + 1);
                    stream.Write((byte)0);
                }
                else
                {
                    EncodeLength(stream, value.Length - prefixZeros);
                }
                for (var i = prefixZeros; i < value.Length; i++)
                {
                    stream.Write(value[i]);
                }
            }
        }
    }
}