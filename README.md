# 1 Program instruction

## 1.1Function introduction

This program implement a spatiotemporal transmission model to simulate an epidemic(STTM), which could simulation the transmission process of infectious diseases from a macro perspective. Users can understand how to use the model concretely through the two sets of data provided.

## 1.2Datasets

### 1.2.1 Synthetic dataset

The synthetic dataset is used to verify the correctness of the code and demonstrate the use of the model. 

(a) The 16 sub-regions in the study area. (b) Initial population structure data of each sub-region, and  City 2 has the initial patient. (c) Daily population flow data between sub-regions. (d) Simulated infectious disease parameters.

### 1.2.2 Real-world dataset

The real-world dataset will be used to simulate the spatiotemporal transmission process of the COVID-19 epidemic in the Chinese mainland from January 1, 2020 to January 22, 2020. (a) This map shows the spatial distribution of early-stage infections in China, which can be combined with demographic data to construct population structure data. (b) Daily population flow data between cities in China. (c) Real infectious disease parameters.

# 2 Interface display

The following figure shows the interface of the STTM.

## 2.1 The input data

Population structure dataset

Population OD flow dataset

## 2.2 The input parameters

Infection rate of the exposed

Infection rate of the infected

Transformation rate from the exposed to the infected

Transformation rate from the infected to the removed

## 2.3 The output data

Daily result

Statistic result

# 3 Result visualization

## 3.1 Result visualization of synthetic dataset 

Select “Synthetic dataset” for simulation and visualize the results

## 3.2  Result visualization of real-world dataset

Select “Real-world dataset” for simulation and visualize the results 

![](C:\Users\李爱国\Documents\GitHub\STTM\image\9.png