namespace MilCalc;

public readonly struct Result<T, E> {
    private readonly bool _success;
    public readonly T Value;
    public readonly E Error;
    private Result(T v, E e, bool success) {
        Value = v;
        Error = e;
        _success = success;
    }

    public bool IsOk => _success;

    public static Result<T, E> Ok(T v) {
        return new(v, default(E), true);
    }

    public static Result<T, E> Err(E e) {
        return new(default(T), e, false);
    }

    public static implicit operator Result<T,E>(T v) => new(v, default(E), true);
    public static implicit operator Result<T,E>(E e) => new(default(T), e, false);

    public R Match<R>(Func<T, R> success, Func<E,R> failure) => _success ? success(Value) : failure(Error);
}

public static class MilCalc {
    public static void Main() {
        while(true) {
            Console.Write("Meters:");
            string? entry = Console.ReadLine();
            Result<short, string> res = IsCleanInt(entry);
            if(!res.IsOk) {
                Console.WriteLine(res.Error);
                continue;
            }
            //else
            Console.WriteLine(Mils(res.Value) + "mil");
        }
    }

    public static Result<short, string> IsCleanInt(string? str) {
        if(String.IsNullOrEmpty(str)) {
            return "Invalid Data";
        }
        if(String.IsNullOrWhiteSpace(str)) {
            return "Invalid Data";
        }
        try {
            short meters = Convert.ToInt16(str);
            if(meters > 1600) {
                return "Too Large";
            }
            if(meters < 100) {
                return "Too Small";
            }
            return meters;
        }
        catch (FormatException){
            return "Not a whole Number";
        }
        catch (OverflowException) {
            return "Too Large";
        }
    }

    public static int Mils(int meters) {
        return Convert.ToInt32(Math.Round(1001.71d - 0.2376267d * meters));
    }
}
