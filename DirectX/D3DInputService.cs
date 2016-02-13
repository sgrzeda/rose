// pretty much useless in an app like this
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;
using System.Drawing;

namespace Rose.DirectX
{
    enum MouseButton
    {
        LeftButton,
        RightButton,
        MiddleButton
    }

    class D3DInputService
    {
        private Device m_pKeyboardDevice;
        private MouseDevice m_pMouseDevice;

        private KeyboardState m_pCurrentKeyboardState;
        private KeyboardState m_pLastKeyboardState;

        public MouseDevice MouseDevice
        {
            get
            {
                return m_pMouseDevice;
            }
        }
        public KeyboardState KeyboardState
        {
            get
            {
                return m_pCurrentKeyboardState;
            }
        }

        private static D3DInputService m_sD3DInputService;
        public static D3DInputService Instance
        {
            get
            {
                return m_sD3DInputService;
            }
        }

        public static void CreateService(IntPtr hWnd)
        {
            m_sD3DInputService = new D3DInputService(hWnd);
        }

        private D3DInputService(IntPtr hWnd)
        {
            MouseDevice.CreateService(hWnd);
            m_pMouseDevice = MouseDevice.Instance;
            m_pKeyboardDevice = new Device(SystemGuid.Keyboard);
            m_pKeyboardDevice.SetCooperativeLevel(hWnd, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
            m_pKeyboardDevice.Acquire();
            m_pCurrentKeyboardState = m_pLastKeyboardState = m_pKeyboardDevice.GetCurrentKeyboardState();

        }

        public void UpdateDeviceState()
        {
            m_pMouseDevice.UpdateDeviceState();
            m_pLastKeyboardState = m_pCurrentKeyboardState;

            m_pCurrentKeyboardState = m_pKeyboardDevice.GetCurrentKeyboardState();
        }
    }

    class MouseDevice
    {
        private Device m_pMouseDevice;
        private byte[] m_arrCurrButtons;
        private byte[] m_arrLastButtons;
        private MouseState m_pCurrentMouseState;
        private MouseState m_pLastMouseState;

        private static MouseDevice m_sMouseSingleton;

        public static MouseDevice Instance
        {
            get
            {
                return m_sMouseSingleton;
            }
        }

        private MouseDevice(IntPtr hWnd)
        {
            m_pMouseDevice = new Device(SystemGuid.Mouse);
            m_pMouseDevice.SetCooperativeLevel(hWnd, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
            m_pMouseDevice.Acquire();
        }

        public MouseState CurrentMouseState
        {
            get
            {
                return m_pCurrentMouseState;
            }
        }

        public MouseState LastMouseState
        {
            get
            {
                return m_pLastMouseState;
            }
        }

        public Point CurrentLocation
        {
            get
            {
                return new Point(m_pCurrentMouseState.X, m_pCurrentMouseState.Y);
            }
        }

        public void UpdateDeviceState()
        {
            m_pLastMouseState = m_pCurrentMouseState;
            m_arrLastButtons = m_pLastMouseState.GetMouseButtons();
            m_pCurrentMouseState = m_pMouseDevice.CurrentMouseState;
            m_pCurrentMouseState = m_pLastMouseState = m_pMouseDevice.CurrentMouseState;
            m_arrCurrButtons = m_pCurrentMouseState.GetMouseButtons();
        }

        public static void CreateService(IntPtr hWnd)
        {
            m_sMouseSingleton = new MouseDevice(hWnd);
        }

        public bool this[MouseButton button]
        {
            get
            {
                return m_arrCurrButtons[(int)button] != 0;
            }
        }

        public bool IsDrag(MouseButton button)
        {
            return m_arrCurrButtons[(int)button] != 0 && m_arrLastButtons[(int)button] != 0;
        }
    }
}
*/