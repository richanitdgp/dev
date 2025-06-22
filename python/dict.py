mydict = {'k1': 'v1', 'k2': 'v2', 'k3': 'v3'}
print(mydict)
print(mydict.keys())
print(mydict.values())
print(mydict.items())

dict1 = {'k1': 100, 'k2': 200}
dict1['k3'] = 300

print(dict1)

# dictionary can contain varied object types
dict2 = {'k1': 100, 'k2':[1,2,3], 'k3':{'app': 200}}
print(dict2)
print(dict2['k2'])
print(dict2['k2'][1])
print(dict2['k3']['app'])

dict2['k1'] = 1000
print(dict2)