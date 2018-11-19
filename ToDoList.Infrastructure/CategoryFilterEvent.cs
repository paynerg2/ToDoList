using Prism.Events;

namespace ToDoList.Infrastructure
{
    public class CategoryFilterEvent : PubSubEvent<string>
    {
    }
}
