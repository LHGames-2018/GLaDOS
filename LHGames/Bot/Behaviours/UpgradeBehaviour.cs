using System;
using System.Collections.Generic;
using LHGames.Helper;

namespace LHGames.Bot.Behaviours
{
    public class UpgradeBehaviour : Behaviour
    {
        private Stack<UpgradeType> _upgradeList = new Stack<UpgradeType>();
        private int[] _costs;

        public UpgradeBehaviour(BehaviourExecuter executer) : base(executer)
        {

        }

        public override bool Evaluate()
        {
            if (_costs == null)
            {
                Setup();
            }
            return _executer.PlayerInfo.Position == _executer.PlayerInfo.HouseLocation && _executer.PlayerInfo.TotalResources >= _costs[_executer.PlayerInfo.GetUpgradeLevel(_upgradeList.Peek())];
        }

        public override string Execute()
        {
            return AIHelper.CreateUpgradeAction(_upgradeList.Pop());
        }

        private void Setup()
        {
            var upgraded = new Dictionary<UpgradeType, int>();
            upgraded.Add(UpgradeType.AttackPower, _executer.PlayerInfo.GetUpgradeLevel(UpgradeType.AttackPower));
            upgraded.Add(UpgradeType.CarryingCapacity, _executer.PlayerInfo.GetUpgradeLevel(UpgradeType.CarryingCapacity));
            upgraded.Add(UpgradeType.CollectingSpeed, _executer.PlayerInfo.GetUpgradeLevel(UpgradeType.CollectingSpeed));
            upgraded.Add(UpgradeType.Defence, _executer.PlayerInfo.GetUpgradeLevel(UpgradeType.Defence));
            upgraded.Add(UpgradeType.MaximumHealth, _executer.PlayerInfo.GetUpgradeLevel(UpgradeType.MaximumHealth));

            _upgradeList.Push(UpgradeType.CarryingCapacity);
            _upgradeList.Push(UpgradeType.CollectingSpeed);
            _upgradeList.Push(UpgradeType.AttackPower);
            _upgradeList.Push(UpgradeType.CarryingCapacity);
            _upgradeList.Push(UpgradeType.CollectingSpeed);

            while (true)
            {
                if (upgraded[_upgradeList.Peek()] > 0)
                {
                    var up = _upgradeList.Pop();
                    upgraded[up]--;
                    continue;
                }
                break;
            }

            _costs = new int[] { 10000, 15000, 25000, 50000, 100000 };
        }
    }
}