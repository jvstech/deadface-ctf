using System.Net.Sockets;
using System.Text;

namespace matrix
{
  internal class Program
  {
    static void Main(string[] args)
    {
      TcpClient client = new("code.deadface.io", 50000);
      using NetworkStream ns = client.GetStream();
      using StreamReader r = new(ns, Encoding.ASCII, leaveOpen: true);
      using StreamWriter w = new(ns, Encoding.ASCII, leaveOpen: true)
      { NewLine = "\xa" };
      int minSum = Enumerable.Range(0, 5)
        .Select(_ => r.ReadLine())
        .Where(l => l != null)
        .Select(l => l!)
        .Select(row => { Console.WriteLine(row); return row.Trim('[', ']').Split(", "); })
        .Select(items => items
          .Select(i => int.Parse(i))
          .Min())
        .Sum();
      Console.WriteLine($"(Minimum sum: {minSum})");
      w.WriteLine($"{minSum}");
      w.Flush();
      while (!r.EndOfStream)
      {
        Console.Write(r.ReadLine());
      }
    }
  }
}