namespace ConsoleApp
{
    public class Teacher
    {
        public int Id { get; set; }//PK
        public string Name { get; set; } = string.Empty;

        public Education? Teaches { get; set; }//Navigation property

        public override String ToString()
        {
            return "Id:" + Id + ", name:" + Name + "teaches" + Teaches;
        }

    }
}
