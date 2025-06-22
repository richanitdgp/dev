import http.client

url1 = "www.microsoft.com"
url2 = "www.apple.com"
url3 = "www.google.com"

conn1 = http.client.HTTPSConnection(url1)
conn2 = http.client.HTTPSConnection(url2)
conn3 = http.client.HTTPSConnection(url3)

conn1.request("GET", "/")
conn2.request("GET", "/")
conn3.request("GET", "/")

res1 = conn1.getresponse()
res2 = conn2.getresponse()
res3 = conn3.getresponse()

print(f"Response1 Status: {res1.status}, reason: {res1.reason}")
print(f"Response2 Status: {res2.status}, reason: {res2.reason}")
print(f"Response3 Status: {res3.status}, reason: {res3.reason}")

