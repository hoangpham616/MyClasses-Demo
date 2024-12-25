using System.Collections;
using Unity.Mathematics;
using UnityEngine;

#pragma warning disable 0414

namespace MyClasses.Sample.DesignPattern
{
    public class MyClasses_DesignPatterns_Script_State : MyClasses_DesignPatterns_Script_Base
    {
        #region ----- State - State -----

        public abstract class State
        {
            protected AudioPlayer _player;

            public abstract void SetContext(AudioPlayer player);
            public abstract void ClickLock();
            public abstract void ClickPlay();
            public abstract void ClickNext();
            public abstract void ClickPrevious();
        }

        #endregion

        #region ----- State - Concrete States -----

        public class LockedState : State
        {
            public override void SetContext(AudioPlayer player)
            {
                _player = player;
            }

            public override void ClickLock()
            {
                _player.ChangeState(new ReadyState());
            }

            public override void ClickPlay()
            {
            }

            public override void ClickNext()
            {
            }

            public override void ClickPrevious()
            {
            }
        }

        public class ReadyState : State
        {
            public override void SetContext(AudioPlayer player)
            {
                _player = player;
            }

            public override void ClickLock()
            {
                _player.ChangeState(new LockedState());
            }

            public override void ClickPlay()
            {
                _player.StartPlayback();
                _player.ChangeState(new PlayingState());
            }

            public override void ClickNext()
            {
                _player.NextSong();
            }

            public override void ClickPrevious()
            {
                _player.PreviousSong();
            }
        }

        public class PlayingState : State
        {
            public override void SetContext(AudioPlayer player)
            {
                _player = player;
            }

            public override void ClickLock()
            {
                _player.ChangeState(new LockedState());
            }

            public override void ClickPlay()
            {
                _player.StopPlayback();
                _player.ChangeState(new ReadyState());
            }

            public override void ClickNext()
            {
                _player.NextSong();
            }

            public override void ClickPrevious()
            {
                _player.PreviousSong();
            }
        }

        #endregion

        #region ----- State - Context -----

        public class AudioPlayer
        {
            private State _state;
            private int _currentSong;

            public AudioPlayer(State state)
            {
                ChangeState(state);
            }

            public void ChangeState(State state)
            {
                state.SetContext(this);
                _state = state;
            }

            public void ClickLock()
            {
                _state.ClickLock();
            }

            public void ClickPlay()
            {
                _state.ClickPlay();
            }

            public void ClickNext()
            {
                _state.ClickNext();
            }

            public void ClickPrevious()
            {
                _state.ClickPrevious();
            }

            public void StartPlayback()
            {
                Debug.Log("Play");
            }

            public void StopPlayback()
            {
                Debug.Log("Stop");
            }

            public void NextSong()
            {
                _currentSong = Mathf.Min(_currentSong + 1, 10);
                Debug.Log("Song " + _currentSong);
            }

            public void PreviousSong()
            {
                _currentSong = Mathf.Max(_currentSong - 1, 1);
                Debug.Log("Song " + _currentSong);
            }
        }

        #endregion

        #region ----- Implementation -----

        protected override void _Define()
        {
            _name = "State Pattern";
            _complexity = "★";
            _popularity = "★★";
            _defination = "State là mẫu Hành Vi cho phép một đối tượng thay đổi hành vi của nó"
                        + "\nkhi trạng thái bên trong của nó thay đổi.";
            _structure = "- State: interface hoặc abstract class chứa phương thức của tất cả"
                        + "\nConrete States."
                        + "\n- Conrete States: các lớp cụ thể của State, tương ứng các trạng thái"
                        + "\ncủa Context."
                        + "\n- Context: lớp có nhiều trạng thái và hành vi bị thay đổi theo"
                        + "\ntrạng thái.";
            _advantages = "- Nguyên tắc Single Responsibility (SRP): mỗi State tương ứng một lớp"
                        + "\nriêng biệt."
                        + "\n- Nguyên tắc Open/Closed Principle (OCP): có thể thêm State mới mà"
                        + "\nkhông ảnh hưởng Context hoặc các State khác."
                        + "\n- Đơn giản hoá: Context đã loại bỏ các câu lệnh xét trường hợp"
                        + "\n(if-else, switch case).";
            _disadvantages = "- Việc áp dụng sẽ là quá mức cần thiết nếu số lượng trạng thái ít"
                            + "\nhoặc hiếm khi thay đổi.";
        }

        protected override void _Usage()
        {
            AudioPlayer audioPlayer = new AudioPlayer(new ReadyState());
            audioPlayer.ClickPlay();
            audioPlayer.ChangeState(new LockedState());
            audioPlayer.ClickPlay();
            audioPlayer.ChangeState(new PlayingState());
            audioPlayer.ClickPlay();
            audioPlayer.ClickNext();
            audioPlayer.ClickNext();
        }

        #endregion
    }
}