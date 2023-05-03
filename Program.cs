using System.Reflection;

public class APILogic{    
    public static void Main(string[] args) {
        APILogic apiL = new APILogic();
        FrontEndObject feo = new FrontEndObject("Test", "TestUser");
        feo.Created = DateTime.Now;
        BackEndObject beo = new BackEndObject(1, "Test2", "TestUser2");
        beo.Created = DateTime.Now;
        FrontEndObject feoConvertedFromBEO = new FrontEndObject(null, null);
        BackEndObject beoConvertedFromFEO = new BackEndObject(null, null, null);
        
        Console.WriteLine(feoConvertedFromBEO.ToString());
        apiL.ConvertFromBE(ref feoConvertedFromBEO, ref beo);
        Console.WriteLine(feoConvertedFromBEO.ToString());
        
        Console.WriteLine(beoConvertedFromFEO.ToString());
        apiL.ConvertFromFE(ref beoConvertedFromFEO, ref feo);
        Console.WriteLine(beoConvertedFromFEO.ToString());        
    }

    public class FrontEndObject {
        public FrontEndObject(string? name, string? username) {
            this.Name = name == null ? "" : name;
            this.UserName = username == null ? "" : username;
        }
        public string Name {get; set;}
        public DateTime Created {get; set;}
        public string UserName{get; set;}

        public string ToString(){
            string str = "";
            str = "Name:" + this.Name + "; UserName:" + this.UserName + "; Created:" + this.Created;
            return str;
        }
    }

    public class BackEndObject {
        public  BackEndObject(int? id, string? name, string? username) {
            this.ID = id == null ? 0 : id.Value;
            this.Name = name == null ? "" : name;
            this.UserName = username == null ? "" : username;
        }
        public int ID {get; set;}
        public string Name {get; set;}
        public DateTime? Created {get; set;}
        public string UserName{get; set;}
        public string ToString() {
            string str = "";
            str = "Name:" + this.Name + "; UserName:" + this.UserName + "; Created: " + this.Created;
            return str;
        }
    }

    // Use refs to save memory, it's a minimal optimization, but every byte counts. Remember though you're passing a memory reference
    private FrontEndObject ConvertFromBE(ref FrontEndObject to, ref BackEndObject from) {
        PropertyInfo[] props = to.GetType().GetProperties();
        foreach(PropertyInfo prop in props) {
            switch(prop.Name.ToLower()) { // You can opt to use literal cases, but it does make things difficult
                case "created": // You can create special case handlers should you need to
                    to.Created = from.Created == null ? DateTime.UnixEpoch : from.Created.Value;
                    break;
                default: // Use the default case to handle all standard values
                    prop.SetValue(to, from.GetType().GetProperty(prop.Name).GetValue(from));
                    break;
            }
        }
        return to;
    }

    private BackEndObject ConvertFromFE(ref BackEndObject to, ref FrontEndObject from) {
        PropertyInfo[] props = to.GetType().GetProperties();
        foreach(PropertyInfo prop in props) {
            switch(prop.Name.ToLower()) {
                case "id": // ID doesn't exist on your from
                    break;
                case "created":
                    to.Created = from.Created == DateTime.UnixEpoch ? null : from.Created;
                    break;
                default:
                    prop.SetValue(to, from.GetType().GetProperty(prop.Name).GetValue(from));
                    break;
            }
        }
        return to;
    }
}