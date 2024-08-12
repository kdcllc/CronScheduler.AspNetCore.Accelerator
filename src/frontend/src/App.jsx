import { useEffect, useState } from 'react';
import './App.css';
import TimeZoneDropdown from './components/TimeZoneDropdown';
import moment from 'moment-timezone';

function App() {
    const [forecasts, setForecasts] = useState();
    const [timeZone, setTimeZone] = useState(moment.tz.guess());

    const handleTimeZoneChange = (newTimeZone) => {
        setTimeZone(newTimeZone);
    };

    useEffect(() => {
        populateWeatherData();
    }, []);

    const contents = forecasts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.date}>
                        <td>{forecast.date}</td>
                        <td>{forecast.temperatureC}</td>
                        <td>{forecast.temperatureF}</td>
                        <td>{forecast.summary}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tableLabel">CronScheduler Database (jobs)</h1>
            <p>This component demonstrates how to store cron jobs on the db.</p>
            <TimeZoneDropdown onTimeZoneChange={handleTimeZoneChange} defaultTimezone={timeZone} />
            {timeZone}
            {contents}
        </div>
    );
    
    async function populateWeatherData() {
        const response = await fetch('weatherforecast');
        const data = await response.json();
        setForecasts(data);
    }
}

export default App;
