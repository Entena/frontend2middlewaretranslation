# What the hell is this?
This is a simple code sample to help illustrate usage of the C# Reflective functional for translating your frontend objects to backend objects.

# Why do I need this
A common scenario in most WebAPIs is to use two defined classes that share many of the same qualities/fields, but differ due to the level that they live at. For example a backend object might allow a field to be null, but in the frontend you might wish to default that field to a value. Many folks spend a lot of time (and waste a lot of lines of code) writing wrappers to translate between for example.
    
    public class FEObject() {
        public string Name {get; set;}
        public string UserName { get; set;}
        public DateTime Created { get; set;}
    }

    public class BEObject() {
        public int ID { get; }
        public string Name { get; set;}
        public string UserName { get; set;}
        public DateTime Created { get; set;}
    }

    public class APILogic() {
        public ConvertToFE(ref FEObject to, ref BEObject from) {
            to.Name = from.Name;
            to.UserName = from.UserName;
        }
        public ConvertToBE(ref BEObject to, ref BEObject from) {
            to.Name = from.Name;
            to.UserName = from.UserName;
        }
    }

So why is that so bad? Well when you start to have complex objects, or you change your object definitions this translation layer requires an update (and will often times be forgotten). Which leads to some rather brittle and ugly code. I can tell you from experience I have removed hundres of lines of code like this in a professional environment at a FANG company. 

# So what can I do about it
Use the reflective functionality built into C#. This lets you programmatically access class properties and do a lot of beautiful sustainable code. So take a look. 