/*
 * Copyright (c) 2016 Phạm Minh Hoàng
 * Email:       hoangpham61691@gmail.com
 * Framework:   MyClasses
 */





********** DEMO **********

GitHub: https://github.com/hoangpham616/MyClasses-Demo

**************************





********** Compile Flag **********

Ads - Admob: MY_ADMOB | MY_ADMOB_7_1_0 | MY_ADMOB_UNDER_13 | MY_ADMOB_APPLOVIN | MY_ADMOB_MOPUB | MY_ADMOB_UNITY_ADS | MY_ADMOB_VUNGLE

Ads - Unity: MY_UNITY_ADS | MY_UNITY_ADS_DEBUG

IAP: MY_IAP | MY_IAP_DEBUG

Localization: MY_LOCALIZATION_ARABIC | MY_LOCALIZATION_KHMER

Logger: MY_LOGGER_ALL | MY_LOGGER_INFO | MY_LOGGER_WARNING | MY_LOGGER_ERROR

Sound: MY_SOUND_DEBUG

UGUI: MY_UI_ADDRESSABLE | MY_UI_URP | MY_UI_DEBUG

**********************************





********** How to setup **********

1/ On Menu Bar, choose "MyClasses/UGUI/Config ID" to config UI ID

2/ On Menu Bar, choose "MyClasses/UGUI/Config Scenes" to config Unity Scenes, Scenes, HUDs

3/ On Menu Bar, choose "MyClasses/UGUI/Config Popups" to config Popups

4/ On Menu Bar, choose "MyClasses/UGUI/Create MyUGUIBooter" to create MyUGUIBooter

5/ On Hierarchy, choose "MyUGUIBooter" to config Boot Mode

6/ Click "Play" to start

***********************************





********** How to use **********

+ Create prefabs and store them at Resources\Prefabs\UGUI\

+ Genegrate scripts at Menu Bar -> MyClasses -> UGUI -> Create

+ Call methods from MyUGUIManager class:

  - MyUGUIManager.Instance.ShowUnityScene(EUnitySceneID.Main, ESceneID.Lobby)

  - MyUGUIManager.Instance.ShowScene(ESceneID.MainMenu)

  - MyUGUIManager.Instance.ShowPopup(EPopupID.Setting)

  - MyUGUIManager.Instance.ShowFloatPopup(EPopupID.BattleInvite)

  - MyUGUIManager.Instance.ShowLoadingIndicator(10, onTimeOutCallback)

  - MyUGUIManager.Instance.ShowFlyingMessage("This is Flying Message", MyUGUIFlyingMessage.EType.ShortFlyFromBot)

  - MyUGUIManager.Instance.ShowRunningMessage("This is Running Message", ERunningMessageSpeed.Normal)

  - MyUGUIManager.Instance.ShowToastMessage("This is Toast Message", EToastMessageDuration.Medium)

  - MyUGUIManager.Instance.Back()

********************************





********** How to custom UI **********

+ HUDs: "Resources\Prefabs\UGUI\HUDs\"

+ Scenes: "Resources\Prefabs\UGUI\Scenes\"

+ Popups: "Resources\Prefabs\UGUI\Popups\"

+ Popup Overlay, Loading Indicator, Toast Message, Flying Message, Running Message...: "Resources\Prefabs\UGUI\Specialities\"

**************************************