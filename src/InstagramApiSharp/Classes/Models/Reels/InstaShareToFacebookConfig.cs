using Newtonsoft.Json;

namespace InstagramApiSharp.Classes.Models
{
    public class InstaShareToFacebookConfig : InstaDefaultResponse
    {
        [JsonProperty("default_share_to_fb_enabled")]
        public bool DefaultShareToFbEnabled { get; set; }

        [JsonProperty("show_share_to_fb_prompt_in_creation_flow")]
        public bool ShowShareToFbPromptInCreationFlow { get; set; }

        [JsonProperty("show_share_to_fb_prompt_in_media_viewer")]
        public bool ShowShareToFbPromptInMediaViewer { get; set; }

        [JsonProperty("oa_reuse_on_fb_enabled")]
        public bool OaReuseOnFbEnabled { get; set; }

        [JsonProperty("show_bonus_prompt_in_creation")]
        public bool ShowBonusPromptInCreation { get; set; }

        [JsonProperty("bonus_xar_upsell_declined_via_comms")]
        public bool BonusXarUpsellDeclinedViaComms { get; set; }
    }
}
