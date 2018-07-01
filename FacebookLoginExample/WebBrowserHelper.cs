using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
/*
 * Credit to @santosh  https://stackoverflow.com/users/1206824/santosh
 * https://stackoverflow.com/questions/45778466/how-to-clear-ie-cache-for-specific-site-using-c-sharp-not-using-js-or-jquery?answertab=votes#tab-top
 * 
 */
namespace FacebookLoginExample
{
    public static class WebBrowserHelper
    {
        #region WINAPI        
        [DllImport("wininet", EntryPoint = "DeleteUrlCacheEntryA", SetLastError = true)]
        public static extern bool DeleteUrlCacheEntry(IntPtr lpszUrlName);

        [DllImport("wininet", SetLastError = true)]
        public static extern bool DeleteUrlCacheGroup(long GroupId, int dwFlags, IntPtr lpReserved);

        [DllImport("wininet", EntryPoint = "FindFirstUrlCacheEntryA", SetLastError = true)]
        public static extern IntPtr FindFirstUrlCacheEntry(string lpszUrlSearchPattern, IntPtr lpFirstCacheEntryInfo, ref int lpdwFirstCacheEntryInfoBufferSize);

        [DllImport("wininet", SetLastError = true)]
        public static extern IntPtr FindFirstUrlCacheGroup(int dwFlags, int dwFilter, IntPtr lpSearchCondition, int dwSearchCondition, ref long lpGroupId, IntPtr lpReserved);

        [DllImport("wininet", EntryPoint = "FindNextUrlCacheEntryA", SetLastError = true)]
        public static extern bool FindNextUrlCacheEntry(IntPtr hFind, IntPtr lpNextCacheEntryInfo, ref int lpdwNextCacheEntryInfoBufferSize);

        [DllImport("wininet", SetLastError = true)]
        public static extern bool FindNextUrlCacheGroup(IntPtr hFind, ref long lpGroupId, IntPtr lpReserved);
        #endregion

        [StructLayout(LayoutKind.Explicit)]
        public struct INTERNET_CACHE_ENTRY_INFOA
        {
            [FieldOffset(0)]
            public uint dwStructSize;

            [FieldOffset(4)]
            public IntPtr lpszSourceUrlName;

            [FieldOffset(8)]
            public IntPtr lpszLocalFileName;

            [FieldOffset(12)]
            public uint CacheEntryType;

            [FieldOffset(16)]
            public uint dwUseCount;

            [FieldOffset(20)]
            public uint dwHitRate;

            [FieldOffset(24)]
            public uint dwSizeLow;

            [FieldOffset(28)]
            public uint dwSizeHigh;

            [FieldOffset(32)]
            public System.Runtime.InteropServices.ComTypes.FILETIME LastModifiedTime;

            [FieldOffset(40)]
            public System.Runtime.InteropServices.ComTypes.FILETIME ExpireTime;

            [FieldOffset(48)]
            public System.Runtime.InteropServices.ComTypes.FILETIME LastAccessTime;

            [FieldOffset(56)]
            public System.Runtime.InteropServices.ComTypes.FILETIME LastSyncTime;

            [FieldOffset(64)]
            public IntPtr lpHeaderInfo;

            [FieldOffset(68)]
            public uint dwHeaderInfoSize;

            [FieldOffset(72)]
            public IntPtr lpszFileExtension;

            [FieldOffset(76)]
            public uint dwReserved;

            [FieldOffset(76)]
            public uint dwExemptDelta;
        }
        public static void ClearCache()
        {
            bool flag;
            bool flag1;
            long num = (long)0;
            int num1 = 0;
            int num2 = 0;
            IntPtr zero = IntPtr.Zero;
            IntPtr intPtr = IntPtr.Zero;
            bool flag2 = false;
            intPtr = FindFirstUrlCacheGroup(0, 0, IntPtr.Zero, 0, ref num, IntPtr.Zero);
            if ((intPtr == IntPtr.Zero ? true : 259 != Marshal.GetLastWin32Error()))
            {
                while (true)
                {
                    flag = true;
                    if ((259 == Marshal.GetLastWin32Error() ? false : 2 != Marshal.GetLastWin32Error()))
                    {
                        flag2 = DeleteUrlCacheGroup(num, 2, IntPtr.Zero);
                        if ((flag2 ? false : 2 == Marshal.GetLastWin32Error()))
                        {
                            flag2 = FindNextUrlCacheGroup(intPtr, ref num, IntPtr.Zero);
                        }
                        if (flag2)
                        {
                            flag1 = true;
                        }
                        else
                        {
                            flag1 = (259 == Marshal.GetLastWin32Error() ? false : 2 != Marshal.GetLastWin32Error());
                        }
                        if (!flag1)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                intPtr = FindFirstUrlCacheEntry(null, IntPtr.Zero, ref num1);
                if ((intPtr == IntPtr.Zero ? true : 259 != Marshal.GetLastWin32Error()))
                {
                    num2 = num1;
                    zero = Marshal.AllocHGlobal(num2);
                    intPtr = FindFirstUrlCacheEntry(null, zero, ref num1);
                    while (true)
                    {
                        flag = true;
                        INTERNET_CACHE_ENTRY_INFOA structure = (INTERNET_CACHE_ENTRY_INFOA)Marshal.PtrToStructure(zero, typeof(INTERNET_CACHE_ENTRY_INFOA));
                        if (259 != Marshal.GetLastWin32Error())
                        {
                            num1 = num2;
                            flag2 = DeleteUrlCacheEntry(structure.lpszSourceUrlName);
                            if (!flag2)
                            {
                                flag2 = FindNextUrlCacheEntry(intPtr, zero, ref num1);
                            }
                            if (!(flag2 ? true : 259 != Marshal.GetLastWin32Error()))
                            {
                                break;
                            }
                            else if ((flag2 ? false : num1 > num2))
                            {
                                num2 = num1;
                                zero = Marshal.ReAllocHGlobal(zero, (IntPtr)num2);
                                flag2 = FindNextUrlCacheEntry(intPtr, zero, ref num1);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    Marshal.FreeHGlobal(zero);
                }
            }
        }
        /// <summary>
        /// For specific url
        /// </summary>
        /// <param name="url"></param>
        public static void ClearForSpecificUrl(string url)
        {
            try
            {
                int num = 0;
                var intPtr = FindFirstUrlCacheEntry(url, IntPtr.Zero, ref num);
                DeleteUrlCacheEntry(intPtr);
            }
            catch (Exception ex)
            {
                ex.PrintException("ClearForSpecificUrl");
            }
        }
    }
}
