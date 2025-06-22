s = "Happy Thursday"
print(s)

print(s[6:])
print(s[:5])
print(s[1:5]) #print substring from index 1 to 5

print(s[::]) # print original string
print(s[::2]) # print alternate chars
print(s[::-1]) # reverse

s1 = "Hello..."
s2 = s1 + "it is beautiful outside!!"
print(s2)

print(s2.upper())
print(s2.lower())
print(s2.split())
print(s2.split('i'))

print("Name of those friends were {0}, {1} and {2}".format("Bob", "Tom", "Alice"))

name = "Tom"
print(f"His name is {name}")

age = 3
print(f"{name} is {age} years old")