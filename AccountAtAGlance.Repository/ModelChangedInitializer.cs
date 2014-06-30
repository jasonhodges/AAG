using System.Data.Entity;

namespace AccountAtAGlance.Repository
{
    public class ModelChangedInitializer : DropCreateDatabaseIfModelChanges<AccountAtAGlanceContext>
    {
        protected override void Seed(AccountAtAGlanceContext context)
        {
            DataInitializer.Initialize(context);
            base.Seed(context);
        }
    }
}
