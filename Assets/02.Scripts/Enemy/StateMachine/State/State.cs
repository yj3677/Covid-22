using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : MonoBehaviour
{
    public Action[] actions;
    public Transition[] transition;
    public Color sceneGizmoColor = Color.gray;
}
