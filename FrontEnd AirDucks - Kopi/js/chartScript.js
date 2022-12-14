function setValues(switchArgument){
  let xValues = []
  let yValues = []
  let measData = []
  let chartData = []
  let today = new Date()
  let oneDayBack = new Date(today.setDate(today.getDate() - 1))
  let oneWeekBack = new Date(today.setDate(today.getDate() - 7))
  let oneMonthBack = new Date(today.setDate(today.getDate() - 31))


  measData = JSON.parse(document.getElementById("data").innerText)
  measData.forEach(meas => {
    chartData.push({time: new Date(meas.timeStamp), reading: meas.reading})
  })
  switch(switchArgument) {
      case 1:
      chartData.forEach(measurement => {
        dateToCheck = measurement.time
        if(dateToCheck >= oneDayBack)
        {          
          xValues.push(measurement.time)
          yValues.push(measurement.reading)
        }

      });
        break;
      case 2:
      chartData.forEach(measurement => {
        dateToCheck = measurement.time
        if(dateToCheck >= oneWeekBack)
        {          
          xValues.push(measurement.time)
          yValues.push(measurement.reading)
        }

      });
        break;
      case 3:
        chartData.forEach(measurement => {
          dateToCheck = measurement.time
          if(dateToCheck >= oneMonthBack)
          {          
            xValues.push(measurement.time)
            yValues.push(measurement.reading)
          }
  
        });
        break;
      case 4:
        chartData.forEach(measurement => {
          xValues.push(measurement.time)
          yValues.push(measurement.reading)
  
        });
        break;
      default:
        // code block
    }
  new Chart("myChart", {
    type: "line",
    data: {
      labels: xValues,
      datasets: [{
        fill: false,
        lineTension: 0,
        backgroundColor: "rgba(0,0,255,1.0)",
        borderColor: "rgba(0,0,255,0.1)",
        data: yValues
      }]
    },
    options: {
      legend: {display: false},
      scales: {
        yAxes: [{ticks: {min: 0, max:100}}],
      }
    }
  });
}