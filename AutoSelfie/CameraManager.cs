using System;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace AutoSelfie
{
    public static unsafe class CameraManager
    {
        [PluginService] public static ITargetManager TargetManager { get; private set; } = null!;

        private static IntPtr CameraPointer => TargetManager?.Camera?.Address ?? IntPtr.Zero;

        public static void SetCamera(float yawDegrees, float pitchDegrees, float zoomDistance)
        {
            if (CameraPointer == IntPtr.Zero)
                return;

            var camera = (Camera*)CameraPointer;
            camera->Yaw = DegreesToRadians(yawDegrees);
            camera->Pitch = DegreesToRadians(pitchDegrees);
            camera->Zoom = zoomDistance;
        }

        private static float DegreesToRadians(float degrees)
        {
            return degrees * ((float)Math.PI / 180f);
        }

        private struct Camera
        {
            public fixed byte Unknown1[0x30]; // Padding up to Zoom
            public float Zoom;   // 0x30
            public fixed byte Unknown2[0x40]; // Padding up to Yaw
            public float Yaw;    // 0x70
            public float Pitch;  // 0x74
        }
    }
}
