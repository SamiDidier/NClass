using System;

namespace NReflectTest.Tests
{
  class EventTests
  {
    public event EventHandler PublicEvent;
    private event EventHandler PrivateEvent;
    protected event EventHandler ProtectedEvent;
    internal event EventHandler InternalEvent;
    event EventHandler Event;
  }
}
