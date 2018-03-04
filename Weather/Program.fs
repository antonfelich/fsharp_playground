// Learn more about F# at http://fsharp.org

open System
open System.Net.Http
open System.IO
open FSharp.Data

type Current = XmlProvider<"""
<current>
    <city id="2643743" name="London">
        <coord lon="-0.13" lat="51.51"/>
        <country>GB</country>
        <sun rise="2017-01-30T07:40:36" set="2017-01-30T16:47:56"/>
    </city>
    <temperature value="280.15" min="278.15" max="281.15" unit="kelvin"/>
    <humidity value="81" unit="%"/>
    <pressure value="1012" unit="hPa"/>
    <wind>
        <speed value="4.6" name="Gentle Breeze"/>
        <gusts/>
        <direction value="90" code="E" name="East"/>
    </wind>
    <clouds value="90" name="overcast clouds"/>
    <visibility value="10000"/>
    <precipitation mode="no"/>
    <weather number="701" value="mist" icon="50d"/>
    <lastupdate value="2017-01-30T15:50:00"/>
</current>
""">

[<Measure>] 
type Kelvin =
    static member ToCelsius kelvin =
        (kelvin - 273.15M<Kelvin>) * 1.0M<Celsius/Kelvin>

and [<Measure>] Celsius = 
    static member ToKelvin celsius =
        (celsius + 273.15M<Celsius>) * 1.0M<Kelvin/Celsius>

let convert (kelvin:decimal) : decimal =
    kelvin - 271M

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    let location = argv |> Array.head

    let appid = "4fa444c61b9e38bd739b49f794951382"
    let url = sprintf "https://api.openweathermap.org/data/2.5/weather?q=%s&appid=%s&mode=xml" location appid

    use request:HttpClient = new HttpClient()

    async {
        let! xml = request.GetStringAsync(url) |> Async.AwaitTask

        let weather = xml |> Current.Parse
        (weather.Temperature.Value * 1M<Kelvin>) |> Kelvin.ToCelsius |> System.Console.WriteLine

        System.Console.ReadLine() |> ignore
    }
    |> Async.RunSynchronously

    0 // return an integer exit code