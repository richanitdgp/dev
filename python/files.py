# Traceback (most recent call last):
#  File "c:\mycode\python\files.py", line 1, in <module>
 #   myfile = open('hello.py')
 #            ^^^^^^^^^^^^^^^^
#FileNotFoundError: [Errno 2] No such file or directory: 'hello.py'
# To run this py file without this error make sure to cd to dir where file.txt exists

myfile = open('myfile.txt')
print(myfile.read())

myfile.seek(0)
print(myfile.read())

myfile.seek(0)
print(myfile.readlines())

myfile.close()

with open('myfile.txt') as f:
    print(f.readlines())

with open('myfile.txt', 'r') as f:
    contents = f.read()
    print(contents)

with open('myfile.txt', 'a') as f:
    f.write('this is the fourth line')

with open('myfile.txt', 'r') as f:
    contents = f.read()
    print(contents)

with open('myfile2.txt', 'w') as f:
    f.write('this is a new file')

with open('myfile2.txt', 'r') as f:
    contents = f.read()
    print(contents)