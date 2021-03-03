using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.Managers.Datas
{
    using Global.Components;

    /// <summary>
    /// Class for saving application states.
    /// The state is, for example, the name of the character,
    /// the amount of money the player has,
    /// the saving of the game, the settings,
    /// all the data that can change during the game and that need to be saved.
    /// </summary>
    [Serializable]
    public class DynamicData
    {
#pragma warning disable
        [SerializeField] private List<Question> questions;
#pragma warning restore

        public List<Question> Questions => questions;
    }
}