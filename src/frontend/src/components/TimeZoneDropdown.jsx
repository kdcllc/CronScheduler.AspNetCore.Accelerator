import React from 'react';

import { useState, useEffect } from 'react';
import moment from 'moment-timezone';
import {
    Dropdown,
    Option,
    makeStyles,
    useId,
      } from "@fluentui/react-components";

const useStyles = makeStyles({
root: {
    // Stack the label above the field with a gap
    display: "grid",
    gridTemplateRows: "repeat(1fr)",
    justifyItems: "start",
    gap: "2px",
    maxWidth: "400px",
},
});

const TimeZoneDropdown = ({ onTimeZoneChange, defaultTimezone }) => {
    const [selectedTimezone, setSelectedTimezone] = useState(defaultTimezone);
    const dropdownId = useId("dropdown-default");
    const styles = useStyles();

    const timezoneOptions = moment.tz.names().map(tz => {
        const offset = moment.tz(tz).format('Z');
        return {
          key: tz,
          text: `(UTC${offset}) ${tz}`,
        };
      });

    const onActiveOptionChange = React.useCallback(
    (_, data) => {
        setSelectedTimezone(data?.nextOption?.text);
        onTimeZoneChange(data?.nextOption?.text);
    },
    [setSelectedTimezone, onTimeZoneChange]
    );

    useEffect(() => {
        setSelectedTimezone(defaultTimezone);
    }, [defaultTimezone]);

    return (
        <div className={styles.root}>
            <label id={dropdownId}>Time Zones</label>
            <Dropdown
                aria-labelledby={dropdownId}
                label="Select a timezone"
                onActiveOptionChange={onActiveOptionChange}
                defaultSelectedOptions={[selectedTimezone]}
                defaultValue={selectedTimezone}
            >
            {timezoneOptions.map((option) => (
            <Option key={option.key}>
                {option.text}
            </Option>
            ))}
            </Dropdown>
        </div>

    );
};

export default TimeZoneDropdown;
