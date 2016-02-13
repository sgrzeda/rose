using System;
using Microsoft.DirectX.Direct3D;
using System.Drawing;

namespace XorNet.Rose.DirectX
{
    class D3DDeviceService
    {
        
        private static D3DDeviceService D3DDeviceServiceSingleton;
        private Device D3DDevice;
        private PresentParameters m_pp;
        private bool isDeviceLost;

        public static D3DDeviceService Instance
        {
            get { return D3DDeviceServiceSingleton; }
        }

        public Device DeviceInstance
        {
            get { return D3DDevice; }
        }


        private D3DDeviceService(IntPtr windowIntPtr)
        {
            isDeviceLost = false;
            m_pp = new PresentParameters
                                       {
                                           Windowed = true, 
                                           SwapEffect = SwapEffect.Discard,
                                           PresentationInterval = PresentInterval.One,
                                           BackBufferFormat = Manager.Adapters.Default.CurrentDisplayMode.Format,
                                           BackBufferWidth = Manager.Adapters.Default.CurrentDisplayMode.Width,
                                           BackBufferHeight = Manager.Adapters.Default.CurrentDisplayMode.Height,
                                       };

            CreateFlags flags = new CreateFlags();
            Caps caps = Manager.GetDeviceCaps(0, DeviceType.Hardware);

            if(caps.DeviceCaps.SupportsHardwareTransformAndLight)
                flags |= CreateFlags.HardwareVertexProcessing;
            else
                flags |= CreateFlags.SoftwareVertexProcessing;

            if(caps.DeviceCaps.SupportsPureDevice)
                flags |= CreateFlags.PureDevice;

            D3DDevice = new Device(0, caps.DeviceType, windowIntPtr, flags, m_pp);
        }

        public static void CreateDeviceService(IntPtr handle)
        {
            if (D3DDeviceServiceSingleton == null)
                D3DDeviceServiceSingleton = new D3DDeviceService(handle);
        }

        public bool IsDeviceAvailable()
        {
            if (D3DDevice == null)
                return false;
            if (D3DDevice.Disposed)
                return false;

            if (isDeviceLost)
            {
                RecoverDevice();
                return false;
            }

            return true;
        }

        public bool BeginRender()
        {
            if (IsDeviceAvailable())
            {
                D3DDevice.Clear(ClearFlags.Target, Color.White, 1.0f, 1);
                D3DDevice.BeginScene();
                return true;
            }
            return false;
        }

        public void EndRender(IntPtr handle, int Width, int Height)
        {
            if (!IsDeviceAvailable())
            {
            }
            else
            {
                D3DDevice.EndScene();
                try
                {
                    D3DDevice.Present(new Rectangle(0, 0, Width, Height), handle, true);
                }
                catch (DeviceLostException)
                {
                    isDeviceLost = true;
                }
            }
        }

        public void RecoverDevice()
        {
            int cooperativeLevel;
            D3DDevice.CheckCooperativeLevel(out cooperativeLevel);

            switch((ResultCode)cooperativeLevel)
            {
                case ResultCode.DeviceLost:
                    isDeviceLost = true;
                    break;
                case ResultCode.Success:
                    isDeviceLost = false;
                    break;
                case ResultCode.DeviceNotReset:
                    try
                    {
                        D3DDevice.Reset(m_pp);
                        D3DTextureManager.Instance.DeviceReset();
                        isDeviceLost = false;
                    }
                    catch (DeviceLostException)
                    {
                        isDeviceLost = true;
                    }
                    break;

            }
        }
    }
}
