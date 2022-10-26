using System;
using _Scripts.Helpers.Audio;

namespace Misc.Audios
{
    public static class AudioEvents
    {
        
        # region SFX
        
        public static Action WrongSound = () => AudioManager.Instance.PlaySound(AudioInventory.Instance.wrongAudio);
        public static Action RightSound  = () => AudioManager.Instance.PlaySound(AudioInventory.Instance.rightAudio);
        public static Action WonSound = () => AudioManager.Instance.PlaySound(AudioInventory.Instance.wonSound);
        public static Action LooseSound = () => AudioManager.Instance.PlaySound(AudioInventory.Instance.looseSound);
        public static Action WalkSound;
        
        #endregion

        #region Music

        public static Action MenuMusic;

        #endregion
        

    }
}