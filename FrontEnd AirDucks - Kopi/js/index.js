const baseUrl = "http://localhost:5039/api/AirDucks"
Vue.createApp({
    data() {
        return {
            showAllPageOn: true,
            createPageOn: false,
            updatePageOn: false,
            deletePageOn:false,
            chartPageOn: false,
            data: [],
            measData: [],
            sensorData: [],
            SensorIdToGetBy:null,
            error:null,
            xArray: [],
            yArray: [],
            
            sensorDeleteId: 0,
            deleteMessage: "",
            
            responseMessage: "",
            newSensor: {   
                id: undefined,  
                name: undefined,
                mac: undefined,
            }
        }

    },

    async created() {
        this.getAll();
     },

    methods: {  
        async getAll() {
            try {
                const response = await axios.get(baseUrl)
                this.data = await response.data
                this.measData = this.data.measurements
                this.sensorData = this.data.Sensors
                this.sensorData.forEach(sensor => {
                    this.measData.forEach(meas => {
                        if(meas.SensorId === sensor.Id){
                            sensor.latestMeasurement = meas.Reading
                        }
                    }) 
                });
            } catch (ex) {
                alert(ex.message)
            }
            this.ShowAllPage()
        },
        async addSensor() {
            console.log(this.newSensor)
            try {
                const response = await axios.post(baseUrl, this.newSensor);
                this.addMessage = "response " + response.status + " " + response.statusText
                this.getAll();
            } catch(ex) {
                alert(ex);
            }
        },
        async deleteSensor() {
            try {
                const deleteUrl = `${baseUrl}`
                const response = await axios.delete(deleteUrl, { data: this.newSensor });
                this.getAll();
            } catch(ex) {
                alert(ex);
            }
        },
        async updateSensor() {
            const updateUrl = `${baseUrl}`
            console.log(this.newSensor)
            try {
                const response = await axios.put(updateUrl, this.newSensor);
                this.getAll();
            } catch(ex) {
                alert(ex);
            }
        },
        createSensorPage() {
            this.createPageOn = true;
            this.showAllPageOn = false;

            if(this.createPageOn = true) 
            {
                this.showAllPageOn = false;
                this.updatePageOn = false;
                this.deletePageOn = false;
            }
        },
        updateSensorPage(id, name, mac) {
            this.updatePageOn = true;
            this.showAllPageOn = false;
            if(this.updatePageOn = true) 
            {
                console.log(id, name, mac)
                this.newSensor.id = id;
                this.newSensor.name = name;
                this.newSensor.mac = mac;
                this.showAllPageOn = false;
                this.createPageOn = false;
                this.deletePageOn = false;
            }
        },
        ShowAllPage() {
            this.updatePageOn = false;
            this.showAllPageOn = true;
            if(this.showAllPageOn = true) 
            {
                this.updatePageOn = false;
                this.createPageOn = false;
                this.deletePageOn = false;
                this.ChartPageOn = false
            }
        },
        showChartPage(id) {
            this.chartPageOn = true;
            this.showAllPageOn = false;
            if(this.chartPageOn = true) 
            {
                this.updatePageOn = false;
                this.createPageOn = false;
                this.deletePageOn = false;
            }
            this.createChart(id)
        },
        deleteSensorPage(id, name, mac) {
            this.deletePageOn = true;
            this.showAllPageOn = false;
            this.newSensor.id = id;
            this.newSensor.name = name;
            this.newSensor.mac = mac;
            if(this.showAllPageOn = true) 
            {
                this.updatePageOn = false;
                this.createPageOn = false;
                this.showAllPageOn = false;
            }
        },
        async createChart(id)
        {
            try {
                const response = await axios.get(baseUrl + "/" + id)
                this.measData = await response.data;
                /*this.measData.forEach(meas => {
                    if(meas.SensorId === sensor.Id){
                        sensor.latestMeasurement = meas.Reading
                    }
                })*/
                console.log("measData: " + this.measData.reading)
                console.log("measData: " + this.measData.timeStamp)

            } catch (ex) {
                alert(ex.message)
            }
        }

    }

}).mount("#app")
