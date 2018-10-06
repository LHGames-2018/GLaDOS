using System;
using System.Collections.Generic;
using LHGames.Helper;

namespace LHGames.Bot.Behaviours
{
    public class UpgradeBehaviour : Behaviour
    {
        private Stack<UpgradeType> _upgradeList;
        private int[] _costs;

        public UpgradeBehaviour(BehaviourExecuter executer) : base(executer)
        {
            var upgraded = new Dictionary<UpgradeType, int>();
            upgraded[UpgradeType.AttackPower] = _executer.PlayerInfo.GetUpgradeLevel(UpgradeType.AttackPower);
            upgraded[UpgradeType.CarryingCapacity] = _executer.PlayerInfo.GetUpgradeLevel(UpgradeType.CarryingCapacity);
            upgraded[UpgradeType.CollectingSpeed] = _executer.PlayerInfo.GetUpgradeLevel(UpgradeType.CollectingSpeed);
            upgraded[UpgradeType.Defence] = _executer.PlayerInfo.GetUpgradeLevel(UpgradeType.Defence);
            upgraded[UpgradeType.MaximumHealth] = _executer.PlayerInfo.GetUpgradeLevel(UpgradeType.MaximumHealth);

            _upgradeList.Push(UpgradeType.CarryingCapacity);
            _upgradeList.Push(UpgradeType.CollectingSpeed);
            _upgradeList.Push(UpgradeType.AttackPower);
            _upgradeList.Push(UpgradeType.CarryingCapacity);
            _upgradeList.Push(UpgradeType.CollectingSpeed);

            var ok = true;
            while (ok)
            {
                if (upgraded[_upgradeList.Peek()] > 0)
                {
                    var up = _upgradeList.Pop();
                    upgraded[up]--;
                }
            }

            _costs = new int[]{10000, 15000, 25000, 50000, 100000};
        }

        public override bool Evaluate()
        {
            return _executer.PlayerInfo.TotalResources >= _costs[_executer.PlayerInfo.GetUpgradeLevel(_upgradeList.Peek())];
        }

        public override string Execute()
        {
            if (_executer.PlayerInfo.HouseLocation != _executer.PlayerInfo.Position)
            {
                //return MoveToHome();
            }

            return AIHelper.CreateUpgradeAction(_upgradeList.Pop());
        }
    }
}