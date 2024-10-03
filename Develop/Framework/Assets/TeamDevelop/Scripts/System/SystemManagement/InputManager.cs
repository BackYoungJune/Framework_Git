/*********************************************************					
* InputManager.cs					
* 작성자 : YoungJune					
* 작성일 : 2024.01.30 오후 1:28					
**********************************************************/
using UnityEngine;
namespace Dev_System
{
    public class InputManager
    {
        //--------------------------------------------------------					
        // 외부 참조 함수 & 프로퍼티					
        //--------------------------------------------------------					
        public static bool Q_KeyDown { get; private set; }
        public static bool W_KeyDown { get; private set; }
        public static bool E_KeyDown { get; private set; }
        public static bool R_KeyDown { get; private set; }
        public static bool T_KeyDown { get; private set; }
        public static bool U_KeyDown { get; private set; }
        public static bool I_KeyDown { get; private set; }
        public static bool A_KeyDown { get; private set; }
        public static bool A_Key { get; private set; }
        public static bool D_KeyDown { get; private set; }
        public static bool D_Key { get; private set; }
        public static bool K_KeyDown { get; private set; }
        public static bool J_KeyDown { get; private set; }
        public static bool Z_KeyDown { get; private set; }
        public static bool X_KeyDown { get; private set; }
        public static bool C_KeyDown { get; private set; }
        public static bool V_KeyDown { get; private set; }
        public static bool Alpha1_KeyDown { get; private set; }
        public static bool Alpha2_KeyDown { get; private set; }
        public static bool Alpha3_KeyDown { get; private set; }
        public static bool Alpha4_KeyDown { get; private set; }
        public static bool Alpha5_KeyDown { get; private set; }
        public static bool Num7_KeyDown { get; private set; }
        public static bool Num8_KeyDown { get; private set; }
        public static bool Num9_KeyDown { get; private set; }
        public static bool F1_KeyDown { get; private set; }
        public static bool F2_KeyDown { get; private set; }
        public static bool F3_KeyDown { get; private set; }
        public static bool F4_KeyDown { get; private set; }
        public static bool F5_KeyDown { get; private set; }
        public static bool F6_KeyDown { get; private set; }
        public static bool F7_KeyDown { get; private set; }
        public static bool F8_KeyDown { get; private set; }
        public static bool F9_KeyDown { get; private set; }
        public static bool F10_KeyDown { get; private set; }
        public static bool F11_KeyDown { get; private set; }
        public static bool F12_KeyDown { get; private set; }
        public static bool Escape_KeyDown { get; private set; }
        public static bool RightShift_KeyDown { get; private set; }
        public static bool Tab_KeyDown { get; private set; }
        public static bool Space_KeyDown { get; private set; }
        public static bool Return_KeyDown { get; private set; }

        //--------------------------------------------------------					
        // 내부 필드 변수					
        //--------------------------------------------------------					
        private static KeyCode q_Key = KeyCode.Q;
        private static KeyCode w_Key = KeyCode.W;
        private static KeyCode e_Key = KeyCode.E;
        private static KeyCode r_Key = KeyCode.R;
        private static KeyCode t_Key = KeyCode.T;
        private static KeyCode u_Key = KeyCode.U;
        private static KeyCode i_Key = KeyCode.I;
        private static KeyCode a_Key = KeyCode.A;
        private static KeyCode d_Key = KeyCode.D;
        private static KeyCode k_Key = KeyCode.K;
        private static KeyCode j_Key = KeyCode.J;
        private static KeyCode z_Key = KeyCode.Z;
        private static KeyCode x_Key = KeyCode.X;
        private static KeyCode c_Key = KeyCode.C;
        private static KeyCode v_Key = KeyCode.V;
        private static KeyCode alpha1_KeyDown = KeyCode.Alpha1;
        private static KeyCode alpha2_KeyDown = KeyCode.Alpha2;
        private static KeyCode alpha3_KeyDown = KeyCode.Alpha3;
        private static KeyCode alpha4_KeyDown = KeyCode.Alpha4;
        private static KeyCode alpha5_KeyDown = KeyCode.Alpha5;
        private static KeyCode num7_Key = KeyCode.Keypad7;
        private static KeyCode num8_Key = KeyCode.Keypad8;
        private static KeyCode num9_Key = KeyCode.Keypad9;
        private static KeyCode f1_Key = KeyCode.F1;
        private static KeyCode f2_Key = KeyCode.F2;
        private static KeyCode f3_Key = KeyCode.F3;
        private static KeyCode f4_Key = KeyCode.F4;
        private static KeyCode f5_Key = KeyCode.F5;
        private static KeyCode f6_Key = KeyCode.F6;
        private static KeyCode f7_Key = KeyCode.F7;
        private static KeyCode f8_Key = KeyCode.F8;
        private static KeyCode f9_Key = KeyCode.F9;
        private static KeyCode f10_Key = KeyCode.F10;
        private static KeyCode f11_Key = KeyCode.F11;
        private static KeyCode f12_Key = KeyCode.F12;
        private static KeyCode escape_Key = KeyCode.Escape;
        private static KeyCode rightShift_Key = KeyCode.RightShift;
        private static KeyCode tab_Key = KeyCode.Tab;
        private static KeyCode space_Key = KeyCode.Space;
        private static KeyCode return_Key = KeyCode.Return;

        public static void OnUpdate()
        {
            //if (!Input.anyKeyDown) return;

            Q_KeyDown = Input.GetKeyDown(q_Key);
            W_KeyDown = Input.GetKeyDown(w_Key);
            E_KeyDown = Input.GetKeyDown(e_Key);
            R_KeyDown = Input.GetKeyDown(r_Key);
            T_KeyDown = Input.GetKeyDown(t_Key);
            U_KeyDown = Input.GetKeyDown(u_Key);
            I_KeyDown = Input.GetKeyDown(i_Key);
            A_KeyDown = Input.GetKeyDown(a_Key);
            A_Key = Input.GetKey(a_Key);
            D_KeyDown = Input.GetKeyDown(d_Key);
            D_Key = Input.GetKey(d_Key);
            K_KeyDown = Input.GetKey(k_Key);
            J_KeyDown = Input.GetKey(j_Key);
            Z_KeyDown = Input.GetKeyDown(z_Key);
            X_KeyDown = Input.GetKeyDown(x_Key);
            C_KeyDown = Input.GetKeyDown(c_Key);
            V_KeyDown = Input.GetKeyDown(v_Key);
            Alpha1_KeyDown = Input.GetKeyDown(alpha1_KeyDown);
            Alpha2_KeyDown = Input.GetKeyDown(alpha2_KeyDown);
            Alpha3_KeyDown = Input.GetKeyDown(alpha3_KeyDown);
            Alpha4_KeyDown = Input.GetKeyDown(alpha4_KeyDown);
            Alpha5_KeyDown = Input.GetKeyDown(alpha5_KeyDown);
            Num7_KeyDown = Input.GetKeyDown(num7_Key);
            Num8_KeyDown = Input.GetKeyDown(num8_Key);
            Num9_KeyDown = Input.GetKeyDown(num9_Key);
            F1_KeyDown = Input.GetKeyDown(f1_Key);
            F2_KeyDown = Input.GetKeyDown(f2_Key);
            F3_KeyDown = Input.GetKeyDown(f3_Key);
            F4_KeyDown = Input.GetKeyDown(f4_Key);
            F5_KeyDown = Input.GetKeyDown(f5_Key);
            F6_KeyDown = Input.GetKeyDown(f6_Key);
            F7_KeyDown = Input.GetKeyDown(f7_Key);
            F8_KeyDown = Input.GetKeyDown(f8_Key);
            F9_KeyDown = Input.GetKeyDown(f9_Key);
            F10_KeyDown = Input.GetKeyDown(f10_Key);
            F11_KeyDown = Input.GetKeyDown(f11_Key);
            F12_KeyDown = Input.GetKeyDown(f12_Key);
            Escape_KeyDown = Input.GetKeyDown(escape_Key);
            RightShift_KeyDown = Input.GetKeyDown(rightShift_Key);
            Tab_KeyDown = Input.GetKeyDown(tab_Key);
            Space_KeyDown = Input.GetKeyDown(space_Key);
            Return_KeyDown = Input.GetKeyDown(return_Key);

        }

    }//end of class					
}//end of namespace					