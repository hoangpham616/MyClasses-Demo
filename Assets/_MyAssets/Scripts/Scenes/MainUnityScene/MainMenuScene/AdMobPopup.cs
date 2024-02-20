using UnityEngine;
using UnityEngine.EventSystems;
using MyClasses;
using MyClasses.UI;

namespace MyApp
{
    public class AdMobPopup : MyUGUIPopup
    {
        #region ----- Variable -----

        private MyUGUIButton _buttonClose;
        private MyUGUIButton _buttonShowBanner;
        private MyUGUIButton _buttonLoadInterstitial;
        private MyUGUIButton _buttonShowInterstitial;
        private MyUGUIButton _buttonLoadRewardedVideo;
        private MyUGUIButton _buttonShowRewardedVideo;

        #endregion

        #region ----- Constructor -----

        public AdMobPopup(EPopupID id, string prefabNameCanvas, string prefabName3D, string addressableCanvas, string addressable3D, bool isRepeatable = false)
            : base(id, prefabNameCanvas, prefabName3D, addressableCanvas, addressable3D, isRepeatable)
        {
        }

        #endregion

        #region ----- MyUGUIPopup Implementation -----

        public override void OnUGUIInit()
        {
            this.LogInfo("OnUGUIInit", null, ELogColor.DARK_UI);

            base.OnUGUIInit();

            _buttonClose = MyUtilities.FindObject(GameObjectCanvas, "Container/ButtonClose").GetComponent<MyUGUIButton>();
            _buttonShowBanner = MyUtilities.FindObject(GameObjectCanvas, "Container/Buttons/ButtonShowBanner").GetComponent<MyUGUIButton>();
            _buttonLoadInterstitial = MyUtilities.FindObject(GameObjectCanvas, "Container/Buttons/ButtonLoadInterstitial").GetComponent<MyUGUIButton>();
            _buttonShowInterstitial = MyUtilities.FindObject(GameObjectCanvas, "Container/Buttons/ButtonShowInterstitial").GetComponent<MyUGUIButton>();
            _buttonLoadRewardedVideo = MyUtilities.FindObject(GameObjectCanvas, "Container/Buttons/ButtonLoadRewardedVideo").GetComponent<MyUGUIButton>();
            _buttonShowRewardedVideo = MyUtilities.FindObject(GameObjectCanvas, "Container/Buttons/ButtonShowRewardedVideo").GetComponent<MyUGUIButton>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", "popup id = " + MyUGUIManager.Instance.CurrentPopup.ID.ToString(), ELogColor.DARK_UI);

            base.OnUGUIEnter();

            _buttonClose.OnEventPointerClick.AddListener(_OnClickClose);
            _buttonShowBanner.OnEventPointerClick.AddListener(_OnClickShowBanner);
            _buttonLoadInterstitial.OnEventPointerClick.AddListener(_OnClickLoadInterstitial);
            _buttonShowInterstitial.OnEventPointerClick.AddListener(_OnClickShowInterstitial);
            _buttonLoadRewardedVideo.OnEventPointerClick.AddListener(_OnClickLoadRewardedVideo);
            _buttonShowRewardedVideo.OnEventPointerClick.AddListener(_OnClickShowRewardedVideo);

#if USE_MY_ADMOB
            MyAdMobManager.Instance.Initialize(() =>
            {
                this.LogInfo("OnUGUIEnter", "AdMob was initialized", ELogColor.DARK_SDK);
            });
#endif
        }

        public override bool OnUGUIVisible()
        {
            if (base.OnUGUIVisible())
            {
                this.LogInfo("OnUGUIVisible", null, ELogColor.DARK_UI);

                return true;
            }
            return false;
        }

        public override void OnUGUIUpdate(float deltaTime)
        {
        }

        public override void OnUGUIExit()
        {
            this.LogInfo("OnUGUIExit", null, ELogColor.DARK_UI);

            base.OnUGUIExit();

            _buttonClose.OnEventPointerClick.RemoveAllListeners();
            _buttonShowBanner.OnEventPointerClick.RemoveAllListeners();
            _buttonLoadInterstitial.OnEventPointerClick.RemoveAllListeners();
            _buttonShowInterstitial.OnEventPointerClick.RemoveAllListeners();
            _buttonLoadRewardedVideo.OnEventPointerClick.RemoveAllListeners();
            _buttonShowRewardedVideo.OnEventPointerClick.RemoveAllListeners();
        }

        public override bool OnUGUIInvisible()
        {
            if (base.OnUGUIInvisible())
            {
                this.LogInfo("OnUGUIInvisible", null, ELogColor.DARK_UI);
                
                return true;
            }
            return false;
        }

        public override void OnUGUIBackKey()
        {
            MyUGUIManager.Instance.Back();
        }

        #endregion

        #region ----- Button Event -----

        private void _OnClickClose(PointerEventData arg0)
        {
            Hide();
        }

        private void _OnClickShowBanner(PointerEventData arg0)
        {
            this.LogInfo("_OnClickShowBanner", null, ELogColor.UI);

#if USE_MY_ADMOB
            MyAdMobManager.Instance.ShowBanner();
#else
            this.LogError("_OnClickShowBanner", "please add \"USE_MY_ADMOB\" to Define Symbols for using this feature");
#endif
        }

        private void _OnClickLoadInterstitial(PointerEventData arg0)
        {
            this.LogInfo("_OnClickLoadInterstitial", null, ELogColor.UI);

#if USE_MY_ADMOB
            if (!MyAdMobManager.Instance.IsInterstitialAdLoaded() && !MyAdMobManager.Instance.IsInterstitialAdLoading())
            {
                MyAdMobManager.Instance.LoadInterstitialAd(null, () =>
                {
                    this.LogInfo("_OnClickLoadInterstitial().LoadInterstitialAd", "onLoadedCallback", ELogColor.UI);
                }, () =>
                {
                    this.LogInfo("_OnClickLoadInterstitial().LoadInterstitialAd", "onFailedToLoadCallback", ELogColor.UI);
                });
            }
            else
            {
                Debug.Log("AdMobPopup._OnClickLoadInterstitial(): interstitial is loading");
            }
#else
            this.LogError("_OnClickLoadInterstitial", "please add \"USE_MY_ADMOB\" to Define Symbols for using this feature");
#endif
        }

        private void _OnClickShowInterstitial(PointerEventData arg0)
        {
            this.LogInfo("_OnClickShowInterstitial", null, ELogColor.UI);

#if USE_MY_ADMOB
            if (!MyAdMobManager.Instance.IsInterstitialAdLoaded() || MyAdMobManager.Instance.IsInterstitialAdLoading())
            {
                MyUGUIManager.Instance.ShowToastMessage("Interstitial hasn't loaded yet");
            }
            else
            {
                MyAdMobManager.Instance.ShowInterstitialAd(() =>
                {
                    this.LogInfo("_OnClickShowInterstitial().ShowInterstitialAd", "onOpeningCallback", ELogColor.SDK);
                }, () =>
                {
                    this.LogInfo("_OnClickShowInterstitial().ShowInterstitialAd", "onClosedCallback", ELogColor.SDK);
                });
            }
#else
            this.LogError("_OnClickShowInterstitial", "please add \"USE_MY_ADMOB\" to Define Symbols for using this feature");
#endif
        }

        private void _OnClickLoadRewardedVideo(PointerEventData arg0)
        {
            this.LogInfo("_OnClickLoadRewardedVideo", null, ELogColor.UI);

#if USE_MY_ADMOB
            if (!MyAdMobManager.Instance.IsRewardedAdLoaded() && !MyAdMobManager.Instance.IsRewardedAdLoading())
            {
                MyAdMobManager.Instance.LoadRewardedAd(null, () =>
                {
                    this.LogInfo("_OnClickLoadRewardedVideo().LoadRewardedAd", "onLoadedCallback", ELogColor.SDK);
                }, () =>
                {
                    this.LogInfo("_OnClickLoadRewardedVideo().LoadRewardedAd", "onFailedToLoadCallback", ELogColor.SDK);
                });
            }
            else
            {
                this.LogInfo("_OnClickLoadRewardedVideo", "rewarded video is loading", ELogColor.SDK);
            }
#else
            this.LogError("_OnClickLoadRewardedVideo", "please add \"USE_MY_ADMOB\" to Define Symbols for using this feature");
#endif
        }

        private void _OnClickShowRewardedVideo(PointerEventData arg0)
        {
            this.LogInfo("_OnClickShowRewardedVideo", null, ELogColor.UI);

#if USE_MY_ADMOB
            if (!MyAdMobManager.Instance.IsRewardedAdLoaded() || MyAdMobManager.Instance.IsRewardedAdLoading())
            {
                MyUGUIManager.Instance.ShowToastMessage("Rewarded Video hasn't loaded yet");
            }
            else
            {
                MyAdMobManager.Instance.ShowRewardedAd(() =>
                {
                    this.LogInfo("_OnClickShowRewardedVideo().ShowRewardedAd", "onOpeningCallback", ELogColor.SDK);
                }, () =>
                {
                    this.LogInfo("_OnClickShowRewardedVideo().ShowRewardedAd", "onUserEarnedRewardCallback", ELogColor.SDK);
                }, () =>
                {
                    this.LogInfo("_OnClickShowRewardedVideo().ShowRewardedAd", "onFailedToShowCallback", ELogColor.SDK);
                }, () =>
                {
                    this.LogInfo("_OnClickShowRewardedVideo().ShowRewardedAd", "onSkippedCallback", ELogColor.SDK);
                }, () =>
                {
                    this.LogInfo("_OnClickShowRewardedVideo().ShowRewardedAd", "onClosedCallback", ELogColor.SDK);
                });
            }
#else
            this.LogError("_OnClickShowRewardedVideo", "please add \"USE_MY_ADMOB\" to Define Symbols for using this feature");
#endif
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----



        #endregion
    }
}