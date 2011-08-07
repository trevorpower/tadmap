function fireEvents(arr)
{   
   for(var i = 0; i < arr.length; i++)
   {
      eval(arr[i]);
   }
}     