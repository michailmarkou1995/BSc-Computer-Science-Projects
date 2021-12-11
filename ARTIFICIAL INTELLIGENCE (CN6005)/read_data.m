% Read Wisconsin Breast Cancer database
clear; clc; close all;

prompt = 'What is the filename? ';
ID = input(prompt, 's');
if isempty(ID)
    ID = 'you did not specified a name';
    ID;
else
    IDparts = split(ID, '.');
    IDparts = IDparts(2:2);
if (strcmp(IDparts,'csv'))
fh = fopen(ID,'r');
Data = textscan(fh, ['%d,%[^,],',repmat('%f,',1,29),'%f\n'], 'HeaderLines',1);
fclose(fh);
n = length(Data);       % number of features + target
n = n-2;                % ignore first two attributes (ID, target)
P = length(Data{1});    % number of patterns
%% prepare Data sets
p = zeros(n,P);
for pat=1:P
    for i=1:n
        p(i,pat) = double(Data{i+2}(pat));
    end
end

% fitsvm does not read String Labels sto Numeric Mapping instead
t = zeros(1,P); % targets
for pat=1:P
    if char(Data{2}(pat)) == 'M'
        t(pat) = 1; % Malignant
    else
        t(pat) = -1; %Benign
    end
end

save('wdbc.mat', 'p', 't');
%end
elseif (strcmp(IDparts,'xlsx'))
    %[~,fid] = xlsread(ID, 1, 'A2:AF1000');
    
else
    IDparts = 'Wrong Data';
end
end