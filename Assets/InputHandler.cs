using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Burst;
using UnityEngine.SceneManagement;
namespace Game.Input
{
    [BurstCompile]
    public class InputHandler : MonoBehaviour
    {
        public static event Action<InputActionMap> OnMapChanged;
        private static MainInputAction _input;
        public static InputAction Movement => _input.Player.Move;
        public static InputAction Jump => _input.Player.Jump;
        public static InputAction Interact => _input.Player.Interact;
        public static InputActionMap Player => _input.Player;

        private void Awake()
        {
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            _input = new MainInputAction();
            ToggleActionMap(_input.Player);
        }
        private void OnSceneUnloaded(Scene current)
        {
            _input.Disable();
        }

        public static void ToggleActionMap(InputActionMap map)
        {
            if (map.enabled)
                return;

            _input.Disable();
            map.Enable();
            OnMapChanged?.Invoke(map);
        }
    }
}