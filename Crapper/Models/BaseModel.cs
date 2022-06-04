namespace Crapper.Models
{
    /// <summary>
    /// This model will be inherited by other classes, any objects added here will be carried across your models dependant on what type you pass in
    /// </summary>
    /// Clean install of DB entrees to reflect changes:
    /// Step 1: Package manager console:  Update-database -TargetMigration:0 -f (This resets tables to the original migration when you started so theyre clear
    /// Step 2: Update-database (This resets columns etc that you added and sets db to current code migration
    /// <typeparam name="T"></typeparam>
    public class BaseModel<T>
    {
        /// <summary>
        /// This is generic Id, you can apply a type when inheriting this class
        /// </summary>
        public T Id { get; set; }
    }
}
