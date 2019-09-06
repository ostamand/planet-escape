using System.Collections.Generic;

namespace FightAction
{

    public enum Action { Shoot0 = 0, Shoot1 = 1, Idle = 2 }
    public Dictionary<Action, string> ActionLabels = new Dictionary<Action, string>
    {
        { Action.Shoot0, "Shoot0"},
        { Action.Shoot1, "Shoot1"},
        { Action.Idle, "Idle"}
    };
    public Dictionary<int, Action> IndexToAction = new Dictionary<int, Action>
    {
        { 0, Action.Shoot0},
        { 1, Action.Shoot1},
        { 2, Action.Idle}
    };

}