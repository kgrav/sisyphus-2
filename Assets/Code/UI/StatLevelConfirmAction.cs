public class StatLevelConfirmAction : RowSelectAction {
    public override bool OnConfirm()
    {
        bool r = StatManager.slb.totalPoints > 0;
        StatManager.statManager.LockInBean();
        return r;
    }
}