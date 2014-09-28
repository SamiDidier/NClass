using System;
using Gtk;
using GtkSpell;

public class GtkHelloWorld {
 
  public static void Main(string[] args) {
    Application.Init();

    //Create the Window
    Window myWin = new Window("GtkSpell# Sample App");
    myWin.Resize(200,200);
    
    //Create a TextView 
    TextView myTextView = new TextView();
    
    SpellCheck mySpellCheck;
    
    //Bind GtkSpell to our textview
    if (args.Length > 0)
      mySpellCheck = new SpellCheck(myTextView, args[0]);
    else    
      mySpellCheck = new SpellCheck(myTextView, "en-us");

    //spellCheck.Detach();
    //spellCheck.();

    //Add the TextView to the form     
    myWin.Add(myTextView);
    
    //Show Everything     
    myWin.ShowAll();
    
    myWin.DeleteEvent += new DeleteEventHandler(delete);
    
    Application.Run();   
   }
   
   static void delete(object o, DeleteEventArgs args)
   {
    Application.Quit();
   }
}
