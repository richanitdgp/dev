import http.client

connection = http.client.HTTPConnection("www.microsoft.com")

connection.request("GET", "/")
response = connection.getresponse()

print("Status: ", response.status)
print("Reason: ", response.reason)
print("Data: ", response.read().decode())