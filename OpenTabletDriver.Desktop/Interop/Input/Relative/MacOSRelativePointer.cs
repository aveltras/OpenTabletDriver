using System;
using System.Linq;
using System.Numerics;
using OpenTabletDriver.Native.OSX;
using OpenTabletDriver.Native.OSX.Input;
using OpenTabletDriver.Plugin.Platform.Pointer;

namespace OpenTabletDriver.Desktop.Interop.Input.Relative
{
    using static OSX;

    public class MacOSRelativePointer : MacOSVirtualMouse, IRelativePointer, IPressureHandler
    {
        private CGPoint offset;

        public MacOSRelativePointer()
        {
            var primary = DesktopInterop.VirtualScreen.Displays.First();
            offset = new CGPoint(primary.Position.X, primary.Position.Y);
        }

        public void SetPosition(Vector2 delta)
        {
            QueuePendingPosition(delta.X, delta.Y);
        }

        public void SetPressure(float percentage)
        {
            base.setPressure(percentage);
        }

        protected override void SetPendingPosition(IntPtr mouseEvent, float x, float y)
        {
            CGEventSetLocation(mouseEvent, GetCursorPosition() + new CGPoint(x, y));
            CGEventSetDoubleValueField(mouseEvent, CGEventField.mouseEventDeltaX, x);
            CGEventSetDoubleValueField(mouseEvent, CGEventField.mouseEventDeltaY, y);
        }

        protected override void ResetPendingPosition(IntPtr mouseEvent)
        {
            CGEventSetDoubleValueField(mouseEvent, CGEventField.mouseEventDeltaX, 0);
            CGEventSetDoubleValueField(mouseEvent, CGEventField.mouseEventDeltaY, 0);
        }

        private CGPoint GetCursorPosition()
        {
            var eventRef = CGEventCreate(IntPtr.Zero);
            var pos = CGEventGetLocation(eventRef) + offset;
            CFRelease(eventRef);
            return pos;
        }
    }
}
