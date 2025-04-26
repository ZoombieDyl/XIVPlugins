using System;
using Dalamud.IoC;
using Dalamud.Plugin.Services;

namespace AutoSelfie
{
    public static unsafe class CameraManager
    {
        [PluginService]
        private static ICameraManager CameraManagerService { get; set; } = null!;

        private static IntPtr CameraPointer => CameraManagerService->Address ?? IntPtr.Zero;

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
            return (float)(degrees * (Math.PI / 180.0));
        }

        private struct Camera
        {
            public fixed byte Unknown1[0x30];
            public float Zoom;
            public fixed byte Unknown2[0x40];
            public float Yaw;
            public float Pitch;
        }
    }
}
