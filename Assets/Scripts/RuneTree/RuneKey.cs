using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.SkillTree
{
    public enum RuneKey
    {
        NONE = 0,
        
        Q = 1,
        W = 2,
        E = 3,

        I = 4,
        O = 5,
        P = 6,
    }

    public static class RuneKeyHelper
    {
        public static IEnumerable<RuneKey> allValues = 
            Enum.GetValues(typeof(RuneKey))
                .Cast<RuneKey>()
                .Where(it => it != RuneKey.NONE)
                .ToArray();
    }

    public static class RuneKeyExtensions
    {
        public static KeyCode ToKeyCode(this RuneKey runeKey)
        {
            switch (runeKey)
            {
            case RuneKey.Q: return KeyCode.Q;
            case RuneKey.W: return KeyCode.W;
            case RuneKey.E: return KeyCode.E;

            case RuneKey.I: return KeyCode.I;
            case RuneKey.O: return KeyCode.O;
            case RuneKey.P: return KeyCode.P;
            default:
                throw new ArgumentOutOfRangeException(nameof(runeKey), runeKey, null);
            }
        }

        public static RuneKey ToRuneKey(this KeyCode keyCode)
        {
            switch (keyCode)
            {
            case KeyCode.Q: return RuneKey.Q;
            case KeyCode.W: return RuneKey.W;
            case KeyCode.E: return RuneKey.E;
            case KeyCode.I: return RuneKey.I;
            case KeyCode.O: return RuneKey.O;
            case KeyCode.P: return RuneKey.P;
            default:
                throw new ArgumentOutOfRangeException(nameof(keyCode), keyCode, null);
            }
        }
    }
}
