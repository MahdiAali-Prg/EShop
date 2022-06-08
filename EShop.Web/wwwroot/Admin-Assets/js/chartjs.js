
    /////////////////////////////////////////////////////////////////////////////////

    //Get the context of the Chart canvas element we want to select
    var ctx = $("#simple-pie-chart");

    // Chart Options
    var chartOptions = {
        responsive: true,
        maintainAspectRatio: false,
        responsiveAnimationDuration: 500,
    };

    // Chart Data
    var chartData = {
        labels: ['دانلود', 'فروش', 'خریداری شده'],
        datasets: [{
            label: "اولین مجموعه من",
            data: [300, 500, 100],
            backgroundColor: ["rgba(102, 110, 232, 0.8)", "rgba(30, 159, 242, 0.8)", "rgba(255, 95, 32, 0.8)"],
        }]
    };

    var config = {
        type: 'pie',

        // Chart Options
        options: chartOptions,

        data: chartData
    };

    // Create the chart
    var pieSimpleChart = new Chart(ctx, config);

    //////////////////////////////////////////////////////////////////////////////////////
