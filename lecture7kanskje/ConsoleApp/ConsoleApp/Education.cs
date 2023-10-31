namespace ConsoleApp
{
    public class Education
    {
        public int Id { get; set; }//PK
        public string Name { get; set; } = string.Empty;

        public ICollection<Student>? Students { get; set; } //Navigation property
        public override String ToString()
        {
            return "Id:" + Id + ", name:" + Name;
        }
    }
}