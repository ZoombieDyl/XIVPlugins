using System;
using FFXIVClientStructs.FFXIV.Client.Game.Control;

namespace AutoSelfie
{
    public static unsafe class CameraManager
    {
        public static void SetCamera(float yawDegrees, float pitchDegrees, float zoomDistance)
        {
            var camera = CameraManager.Instance()->GetActiveCamera();
            if (camera == null)
                return;

            camera->Yaw = DegreesToRadians(yawDegrees);
            camera->Pitch = DegreesToRadians(pitchDegrees);
            camera->Distance = zoomDistance;
        }

        private static float DegreesToRadians(float degrees)
        {
            return (float)(degrees * (Math.PI / 180.0));
        }
    }
}
