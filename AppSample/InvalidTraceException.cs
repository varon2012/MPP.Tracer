using System;

public class InvalidTraceException : Exception
{
	public InvalidTraceException(string message)
		: base(message)
	{}
}
