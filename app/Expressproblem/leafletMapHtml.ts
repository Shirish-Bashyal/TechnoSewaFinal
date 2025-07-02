// leafletMapHtml.ts
export const leafletMapHtml = `
<!DOCTYPE html>
<html>
<head>
  <meta charset="utf-8" />
  <title>Leaflet Map</title>
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <link
    rel="stylesheet"
    href="https://unpkg.com/leaflet@1.9.4/dist/leaflet.css"
  />
  <style>
    html, body, #map { height: 100%; margin: 0; padding: 0; }
  </style>
</head>
<body>
  <div id="map"></div>
  <script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js"></script>
  <script>
    const map = L.map('map').setView([27.69828, 83.46188], 15);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; OpenStreetMap contributors'
    }).addTo(map);

    let marker = L.marker([27.69828, 83.46188], {draggable: true}).addTo(map);

    // Send message to React Native on marker drag or map click
    function sendCoords(lat, lng) {
      if(window.ReactNativeWebView && window.ReactNativeWebView.postMessage) {
        window.ReactNativeWebView.postMessage(JSON.stringify({latitude: lat, longitude: lng}));
      }
    }

    marker.on('dragend', function(e) {
      const pos = e.target.getLatLng();
      sendCoords(pos.lat, pos.lng);
    });

    map.on('click', function(e) {
      const {lat, lng} = e.latlng;
      marker.setLatLng(e.latlng);
      sendCoords(lat, lng);
    });
  </script>
</body>
</html>
`;
