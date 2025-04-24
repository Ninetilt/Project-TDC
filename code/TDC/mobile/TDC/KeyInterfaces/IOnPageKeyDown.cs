#if ANDROID
using Android.Views;
#endif

namespace TDC
{
    public interface IOnPageKeyDown
    {
        #if ANDROID
                /// <summary>
                /// Called when a key is pressed.
                /// </summary>
                /// <param name="keyCode">The code of pressed key.</param>
                /// <param name="e">Description of the key event.</param>
                /// <returns>
                /// Return true to prevent this event from being propagated further, 
                /// or false to indicate that you have not handled this event and it should continue to be propagated.
                /// </returns>
                public bool OnPageKeyDown(Keycode keyCode, KeyEvent e);
        #endif
    }
}
