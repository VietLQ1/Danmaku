﻿using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.BaseGame.Views.Abstracts.Visualizers
{
    public class DanmakuCardExecutionBaseVisualizer : MonoBehaviour
    {
        public virtual void Visualize(GameObject activatorView, List<GameObject> targetablesView)
        {
            Debug.Log("Visualizer executed!");
        }
    }
}