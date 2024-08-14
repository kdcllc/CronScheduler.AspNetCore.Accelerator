import React from 'react';
import { useEffect, useState } from 'react';
import './App.css';
import TimeZoneDropdown from './components/TimeZoneDropdown';

interface Forecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

function App() {
    const [forecasts, setForecasts] = useState<Forecast[]>();

    const [timezone, setTimezone] = useState<string>('');
    
    const getTimeZone = (): string => {
        return Intl.DateTimeFormat().resolvedOptions().timeZone;
    }
    
    const handleTimezoneChange = (selectedTimezone: string) => {
        setTimezone(selectedTimezone);
    };

    useEffect(() => {
        const userTimezone = getTimeZone();
        setTimezone(userTimezone);
    }, []);

      
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
            <div>
                <h1>Cron Schedule</h1>
                <p><TimeZoneDropdown onTimeZoneChange={handleTimezoneChange} defaultTimezone={timezone} /></p>
                <p>{timezone}</p>
            </div>
            <h1 id="tableLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
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