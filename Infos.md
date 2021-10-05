# Async

## Introducing Async and Await in C#

1. get the response task:<br/>
```
using (var client = new HttpClient())
{
   var responseTask = client.GetAsync>("https://ps-async.fekberg.com/api/stocks/MSFT");
   
   var response = await responseTask;
   
   var content = await response.Content.ReadAsStringAsync();
   
   var data = JsonConvert.DserializeObject<IEnumerable<StockPrice>>(content);
   
   Stocks.ItemsSource = data;
}
```
Achtung: der Aufruf von<br/>
"content = response.Content.ReadAsStringAsync().Result;" blockiert den Ablauf und lässt den Code synchron ablaufen<br/>
Der Aufruf von Result oder Wait() kann einen Deadlock in der Anwendung hervorrufen<br/>

Die Aufrufe können auch bei Datenbanken, Dateien usw. eingesetzt werden.

## Understanding a Continuation

### What does the await keyword really do and how does it affect the flow the the code?
- the "await" keyword allows you to retrieve the result from an asynchronous operation when that result is available
- it makes sure the there were not exceptions or problems with the task that's being awaited
- it is a way to get the result, as well as validate that asynchronous operation
- also it is introducing something called a continuation
  - the continuation is what will be executed after the asynchronouse operation, meaning the code after the "await" keyword
  - this code will run once the taks has completed, and it will run on the same thread that spawned the asynchronous operation, which in our case, is the UI thread
- the <b>await</b> keyword introduces a continuation, allowing you to get back to the orginal context (thread)
- <div style="color:red">updating the UI from different threads is not allowed</div>

## Creating our own asynchronous Method

- keep our methods clean => not too much code
- <div style="color:red">async void is EVIL => only use async void for event handlers</div>
- <div style="color:red">for all other methods use async Task</div>





### Timestamp: 03:10