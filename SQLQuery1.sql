Select* From [S?n Ph?m]
DELETE FROM [S?n Ph?m]; 
DBCC CHECKIDENT (N'[S?n Ph?m]', RESEED, 0);

DELETE FROM [S?n Ph?m];
INSERT INTO [S?n Ph?m] (Loai, TenSanPham, Size, MauSac, Gia, ID_Kho) VALUES
(N'Qu?n', N'Qu?n Jean Nam', N'M', N'Xanh', 350000, 1),
(N'Qu?n', N'Qu?n Jean N?', N'S', N'?en', 320000, 1),
(N'Qu?n', N'Qu?n Short Nam', N'L', N'Tr?ng', 200000, 1),
(N'Qu?n', N'Qu?n Tây Nam', N'XL', N'?en', 400000, 1),
(N'Qu?n', N'Qu?n Jogger', N'M', N'Xám', 280000, 1),
(N'Áo', N'Áo Thun Nam', N'L', N'Tr?ng', 180000, 1),
(N'Áo', N'Áo Thun N?', N'M', N'??', 170000, 1),
(N'Áo', N'Áo S? Mi Nam', N'L', N'Xanh', 300000, 1),
(N'Áo', N'Áo S? Mi N?', N'S', N'H?ng', 290000, 1),
(N'Áo', N'Áo Hoodie', N'XL', N'?en', 450000, 1),
(N'Giày', N'Giày Sneaker Nam', N'42', N'Tr?ng', 700000, 1),
(N'Giày', N'Giày Sneaker N?', N'38', N'?en', 650000, 1),
(N'Giày', N'Giày Sandal Nam', N'41', N'Nâu', 400000, 1),
(N'Giày', N'Giày Sandal N?', N'37', N'Be', 380000, 1),
(N'Giày', N'Giày Th? Thao Nam', N'43', N'Xanh', 750000, 1),
(N'Giày', N'Giày Th? Thao N?', N'39', N'H?ng', 720000, 1),
(N'Giày', N'Giày L??i Nam', N'40', N'Nâu', 620000, 1),
(N'Giày', N'Giày Cao Gót', N'36', N'??', 680000, 1),
(N'Giày', N'Giày Tây Nam', N'42', N'?en', 800000, 1),
(N'Giày', N'Giày Tây N?', N'38', N'Tr?ng', 780000, 1);
