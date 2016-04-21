
window.common = (function () {
    var common = {};

    common.getFragment = function getFragment() {
        if (window.location.hash.indexOf("#") === 0) {
            return parseQueryString(window.location.hash.substr(1));
        } else {
            return {};
        }
    };

    function parseQueryString(queryString) {
        var data = {},
            pairs, pair, separatorIndex, escapedKey, escapedValue, key, value;

        if (queryString === null) {
            return data;
        }

        pairs = queryString.split("&");

        for (var i = 0; i < pairs.length; i++) {
            pair = pairs[i];
            separatorIndex = pair.indexOf("=");

            if (separatorIndex === -1) {
                escapedKey = pair;
                escapedValue = null;
            } else {
                escapedKey = pair.substr(0, separatorIndex);
                escapedValue = pair.substr(separatorIndex + 1);
            }

            key = decodeURIComponent(escapedKey);
            value = decodeURIComponent(escapedValue);

            data[key] = value;
        }

        return data;
    }

    common.toDateTimeString = function (date, format) {
        var monthNames = [
          "January", "February", "March", "April",
          "May", "June", "July", "August",
          "September", "October", "November", "December"
        ];
        date = new Date(date);
        var day = date.getDate(), d = date.toJSON(),
        monthIndex = date.getMonth(),
        year = date.getFullYear(),
        minutes = date.getMinutes(), 
        seconds = date.getSeconds(),
        hours = date.getHours(), afterOrBefore = hours > 12 ? 'PM' : 'AM',
        minutes = minutes.toString().length > 1 ? minutes : '0' + minutes;
        if (/[hh:]/.test(format)) {
            hours = hours > 12 ? hours - 12 : hours || 12;
            hours = hours > 9 ? hours : '0' + hours;
        }
            

        var formats = {
            'MMM dd, yyyy': [monthNames[monthIndex], ' ', day, ', ', year].join(''),
            'MMM dd, yyyy HH:mm:ss': [monthNames[monthIndex], ' ', day, ', ', year, ' ', d.substring(12, 8)].join(''),
            'MMM dd, yyyy hh:mm tt': [monthNames[monthIndex], ' ', day, ', ', year, ' ', hours, ':', minutes, ' ', afterOrBefore].join(''),
            'yyyy-MM-dd hh:mm tt': [d.substring(0, 10), ' ', hours, ':', minutes, ' ', afterOrBefore].join(''),
            'yyyy-MM-dd HH:mm tt': [d.substring(0, 10), ' ', hours, ':', minutes, ' ', afterOrBefore].join(''),
            'MM-dd-yyyy': [monthIndex + 1, '-', day, '-', year].join('')

        };


        return formats[format] ? formats[format] : date.toJSON();

    };

    common.isArray = function(arr){
        return Object.prototype.toString.call(arr) === "[object Array]";
    };

    Array.prototype.orderBy = function (prop, dir) {
        var arr = this;
        if (!common.isArray(arr)) return arr;
        arr = arr.sort(function (a, b) {
            a = a[prop].toLowerCase(),
            b = b[prop].toLowerCase();
            return a === b ? 0 : dir === 'asc' ? (a < b ? -1 : 1) : (a > b ? -1 : 1);
        });
        return arr;
    };

    return common;
})();