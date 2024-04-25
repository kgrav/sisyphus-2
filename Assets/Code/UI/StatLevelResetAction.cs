public class StatLevelResetAction : RowSelectAction {
    public override bool OnConfirm(){
        bool r = StatManager.slb.totalPoints > 0;
        StatManager.slb=new StatLevelBean();
        return r;
    }
}