import os
import random
import csv

# Get the directory of the current script
script_dir = os.path.dirname(os.path.abspath(__file__))

# Define the path for the CSV file relative to the script's directory
csv_file_path = os.path.join(script_dir, 'test09_sextuple.csv')

data = [random.randint(0, 1000) for _ in range(1800)]

# Use the absolute path for the CSV file
with open(csv_file_path, 'w', newline='') as file:
    writer = csv.writer(file)
    for value in data:
        writer.writerow([value])