using System;
using _Scripts.Helpers.Audio;

namespace Misc.Audios
{
    public static class AudioEvents
    {
        
        # region SFX
        
        public static Action Patients = () => AudioManager.Instance.PlaySound(AudioInventory.Instance.pacientes);
        #endregion

        #region Music

        public static Action MenuMusic;

        #endregion
        

    }
}