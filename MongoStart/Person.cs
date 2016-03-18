namespace MongoStart
{
    [MongoInfoMap("Person")]
    internal class Person
    {
        public string Name { get; internal set; }
        public string PassWord { get; internal set; }
        public int Uid { get; internal set; }
    }
}