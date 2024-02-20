using UnityEngine;
using MyClasses;
using MyClasses.UI;
using UnityEngine.UI;

namespace MyApp.UI
{
    public class TopCenterToastNotification : MyUGUIToastNotification
    {
        #region ----- Variable -----

        private Animator _animator;
        private Text _textTitle;

        #endregion

        #region ----- Constructor -----

        public TopCenterToastNotification(EToastNotificationID id, string prefabNameCanvas, string prefabName3D)
            : base(id, prefabNameCanvas, prefabName3D)
        {
        }


        #endregion

        #region ----- MyUGUIToastNotification Implementation -----

        public override void OnUGUIInit()
        {
            this.LogInfo("OnUGUIInit", null, ELogColor.DARK_UI);

            base.OnUGUIInit();

            _animator = GameObjectCanvas.GetComponent<Animator>();
            _textTitle = MyUtilities.FindObject(GameObjectCanvas, "Anchor/Container/Title").GetComponent<Text>();
        }

        public override void OnUGUIEnter()
        {
            this.LogInfo("OnUGUIEnter", ID.ToString(), ELogColor.DARK_UI);

            base.OnUGUIEnter();

            _textTitle.text = "TOAST NOTIFICATION " + AttachedData.ToString();
        }

        public override float OnUGUIAnimationShow()
        {
            this.LogInfo("OnUGUIAnimationShow", null, ELogColor.DARK_UI);

            _animator.Play("Show");

            return 0.75f;
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
        }

        public override float OnUGUIAnimationHide()
        {
            this.LogInfo("OnUGUIAnimationHide", null, ELogColor.DARK_UI);

            _animator.Play("Hide");
            
            return 0.5f;
        }

        public override float OnUGUIAnimationQuickHide()
        {
            this.LogInfo("OnUGUIAnimationQuickHide", null, ELogColor.DARK_UI);

            _animator.Play("QuickHide");
            
            return 0.5f;
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

        public override float GetDisplayTime()
        {
            return 2;
        }

        #endregion

        #region ----- Public Method -----



        #endregion

        #region ----- Private Method -----



        #endregion
    }
}